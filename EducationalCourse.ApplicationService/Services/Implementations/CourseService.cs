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

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class CourseService : ICourseServise
    {
        #region Constructor

        private readonly ICourseRepository _courseRepository;
        private readonly IFileManagerService _fileManagerService;
        private readonly IUnitOfWork _unitOfWork;
        public CourseService(ICourseRepository courseRepository, IFileManagerService fileManagerService, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _fileManagerService = fileManagerService;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        //******************************** SearchCourseByTitle ******************************
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

        //******************************** CreateCourse ************************************
        public async Task<ApiResult> CreateCourse(AddCourseDto request, CancellationToken cancellationToken)
        {
            //ToDo fields validation  
            //Todo BuisinesRule

            var entity = MappingCourseEntity(request);
            await _courseRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد");
        }

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

        //******************************** GetFilterCourses ********************************
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

        //************************************* Filter *************************************
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

        //************************************* AddVideoFileToCourse *************************************
        public async Task<ApiResult> AddVideoFileToCourse(List<AddVideoFileCourseDto> request, CancellationToken cancellationToken)
        {
            //var videoFiles = request.Select(c => c.VideoFiles).ToList();
            //var saveFilesResults = _fileManagerService.SaveFiles(videoFiles, request.First().DirectoryName);

            //var entities = MappingCourseEpisode(request, saveFilesResults);
            throw new AppException();

        }

        private List<CourseEpisode> MappingCourseEpisode(List<AddVideoFileCourseDto> request, List<string> videoNames)
        {
            var result = new List<CourseEpisode>();

            foreach (var item in request)
            {
                result.Add(new CourseEpisode
                {
                    CourseId = item.CourseId,
                    EpisodeTime = item.EpisodeTime,
                    IsFree = item.IsFree,
                    IsActive = true,
                    EpisodeFileName = MappName(videoNames)
                });
            }

            return result;
        }

        private string MappName(List<string> videoNames)
        {
            var EpisodeFileName = string.Empty;

            videoNames.ForEach(c =>
            {
                EpisodeFileName = c;
            });

            return EpisodeFileName;
        }

        public Task<ApiResult> EditCourse(AddCourseDto request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}