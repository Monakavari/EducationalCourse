using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Domain.Dtos;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Dtos.FileManager;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.BasePaging.Dtos;
using EducationalCourse.Framework.CustomException;
using EducationalCourse.Framework.Extensions;
using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class CourseService : ICourseServise
    {
        #region Constructor

        private readonly ICourseRepository _courseRepository;
        private readonly IFileManagerService _fileManagerService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseEpisodeRepository _episodeRepository;
        public CourseService(ICourseRepository courseRepository, IFileManagerService fileManagerService, IUnitOfWork unitOfWork, ICourseEpisodeRepository episodeRepository)
        {
            _courseRepository = courseRepository;
            _fileManagerService = fileManagerService;
            _unitOfWork = unitOfWork;
            _episodeRepository = episodeRepository;
        }

        #endregion Constructor

        //******************************** SearchCourseByTitle *****************************
        public async Task<ApiResult<List<FilterCourseDto>>> SearchCourseByTitle(string courseTitle, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(courseTitle) && courseTitle.Length < 4)
                throw new AppException("تعداد کاراکتر وارد شده باید بیشتر باشد");

            var result = await _courseRepository.GetAllCourse(courseTitle, cancellationToken);

            return new ApiResult<List<FilterCourseDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد");
        }

        //******************************** GetLastCourses ***********************************
        public async Task<ApiResult<List<FilterCourseDto>>> GetLastCourses(CancellationToken cancellationToken)
        {
            var result = await _courseRepository.GetLastCourses(cancellationToken);

            if (result is null)
                throw new AppException("دوره ای یافت نشد");

            return new ApiResult<List<FilterCourseDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }

        //******************************** GetPopularCourses ********************************
        public async Task<ApiResult<List<FilterCourseDto>>> GetPopularCourses(CancellationToken cancellationToken)
        {

            var result = await _courseRepository.GetPopularCourses(cancellationToken);

            if (result is null)
                throw new AppException("دوره ای یافت نشد");


            return new ApiResult<List<FilterCourseDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }

        //******************************** CreateCourse *************************************
        public async Task<ApiResult> CreateCourse(AddCourseDto request, CancellationToken cancellationToken)
        {
            //ToDo fields validation  
            //Todo BuisinesRule

            var entity = MappingCourseEntity(request);
            await _courseRepository.AddAsync(entity, cancellationToken);

            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > default(int);

            if (!result)
                throw new AppException("عملیات ثبت خبر در دیتابیس با خطا مواجه شد");

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد");
        }

        //******************************** MappingCourseEntity ******************************
        private Course MappingCourseEntity(AddCourseDto request)
        {
            var saveImageResult = _fileManagerService.SaveImageCourse(request.FormFile, request.CourseTitle);
            var demoVideoName = _fileManagerService.SaveFile(request.DemoFile, request.DirectoryName);

            return new Course
            {
                CourseGroupId = request.CourseGroupId,
                CourseTitle = request.CourseTitle,
                CourseLevelId = request.CourseLevelId,
                CourseStatusId = request.CourseStetusId,
                TeacherId = request.TeacherId,
                SubCourseGroupId = request.SubCourseGroupId,
                CoursePrice = request.CoursePrice,
                IsFreeCost = request.IsFreeCost,
                CourseImageBase64 = saveImageResult.AvatarBase64,
                CourseImageName = saveImageResult.AvatarName,
                DemoVideo = demoVideoName,
            };

        }

        //******************************** GetFilterCourses *********************************
        public async Task<DataGridResult<FilterCourseResponseDto>> GetFilterCourses(FilterCourseRequestDto request, CancellationToken cancellationToken)
        {
            var result = new DataGridResult<FilterCourseResponseDto>();
            var courseEntity = await Filter(request);

            result.Total = courseEntity.Total;
            result.Data = courseEntity.Data
                .Select(x => new FilterCourseResponseDto
                {
                    CourseTitle = x.CourseTitle,
                    CourseLevelId = x.CourseLevelId,
                    CourseImageBase64 = x.CourseImageBase64,
                    CourseImageName = x.CourseImageName,
                    CourseStatusId = x.CourseStatusId,
                    IsFreeCost = x.IsFreeCost,
                    ViewCount = x.ViewCount,
                    TeacherId = x.TeacherId

                }).ToList();

            return result;
        }

        //************************************* Filter **************************************
        private async Task<DataGridResult<Course>> Filter(FilterCourseRequestDto request)
        {
            var expresion = _courseRepository
                .FetchIQueryableEntity()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.CourseTitle))
                expresion = expresion.Where(x => x.CourseTitle.Contains(request.CourseTitle));

            if (request.CourseGroupId.HasValue && request.CourseGroupId != 0)
                expresion = expresion.Where(x => x.CourseGroupId == request.CourseGroupId.Value);

            if (request.SubCourseGroupId.HasValue && request.SubCourseGroupId != 0)
                expresion = expresion.Where(x => x.SubCourseGroupId == request.SubCourseGroupId.Value);

            if (request.IsFreeCost is true)
                expresion = expresion.Where(x => x.IsFreeCost == request.IsFreeCost);

            if (request.StartPrice.HasValue && request.StartPrice != 0)
                expresion = expresion.Where(x => x.CoursePrice >= request.StartPrice);

            if (request.EndPrice.HasValue && request.EndPrice != 0)
                expresion = expresion.Where(x => x.CoursePrice <= request.EndPrice);

            var filter = await expresion
                .ApplyPaging(new GridState
                {
                    Take = request.Take,
                    Skip = request.Skip,
                    Dir = request.Dir
                });

            return filter;
        }

        //************************************* AddVideoFileToCourse ************************
        public async Task<ApiResult> AddVideoFileToCourse(List<AddVideoFileCourseDto> request, CancellationToken cancellationToken)
        {
            var videoFiles = request.Select(c => c.VideoFiles).ToList();

            var entities = MappingCourseEpisode(request);
            await _episodeRepository.AddRangeAsync(entities, cancellationToken);

            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > default(int);

            if (!result)
                throw new AppException("عملیات ثبت در دیتابیس با خطا مواجه شد");

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد");

        }
        //************************************* MappingCourseEpisode *************************
        private List<CourseEpisode> MappingCourseEpisode(List<AddVideoFileCourseDto> request)
        {
            var result = new List<CourseEpisode>();

            foreach (var item in request)
            {
                var fileName = _fileManagerService.SaveFile(item.VideoFiles, request.First().DirectoryName);
                result.Add(new CourseEpisode
                {
                    CourseId = item.CourseId,
                    EpisodeTime = item.EpisodeTime,
                    IsFree = item.IsFree,
                    IsActive = true,
                    EpisodeFileName = fileName
                });
            }

            return result;
        }

        //************************************* EditCourse ***********************************
        public async Task<ApiResult> EditCourse(EditCourseDto request, CancellationToken cancellationToken)
        {
            //ToDo validation
            //ToDo business validation
            var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);
            var saveImageResult = _fileManagerService.SaveImageCourse(request.FormFile, course.CourseTitle, course.CourseImageName);
            var demoVideoName = _fileManagerService.SaveFile(request.FormFile, request.DirectoryName);

            Course c = new Course();

            c.CourseGroupId = request.CourseGroupId;
            c.SubCourseGroupId = request.SubCourseGroupId;
            c.CourseLevelId = request.CourseLevelId;
            c.CourseStatusId = request.CourseStatusId;
            c.TeacherId = request.TeacherId;
            c.CourseTitle = request.CourseTitle;
            c.CoursePrice = request.CoursePrice;
            c.IsFreeCost = request.IsFreeCost;
            c.CoursePrice = request.CoursePrice;
            c.CourseImageBase64 = saveImageResult.AvatarBase64;
            c.CourseImageName = saveImageResult.AvatarName;
            c.DemoVideo = demoVideoName;

            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > default(int);

            if (!result)
                throw new AppException("عملیات ویرایش در دیتابیس با خطا مواجه شد");

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        public async Task<ApiResult> DeleteCourse(int id,string fileName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> DeleteCourse(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}