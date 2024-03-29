﻿using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Common.Enums;
using EducationalCourse.Common.Extensions;
using EducationalCourse.Domain.Dtos;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Dtos.Wallet;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.BasePaging.Dtos;
using EducationalCourse.Framework.CustomException;
using EducationalCourse.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class CourseService : ICourseServise
    {
        #region Constructor

        private readonly ICourseRepository _courseRepository;
        private readonly ICourseEpisodeService _courseEpisodeService;
        private readonly IFileManagerService _fileManagerService;
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(ICourseRepository courseRepository,
                             IFileManagerService fileManagerService,
                             IUnitOfWork unitOfWork,
                             ICourseEpisodeService courseEpisodeService)
        {
            _courseRepository = courseRepository;
            _fileManagerService = fileManagerService;
            _unitOfWork = unitOfWork;
            _courseEpisodeService = courseEpisodeService;
        }

        #endregion Constructor

        //******************************** GetCourseSinglePageInfo **********************
        public async Task<ApiResult<CourseSinglePageDto>> GetCourseSinglePageInfo(CancellationToken cancellationToken)
        {
            var courseEntity = await _courseRepository.GetCourseSinglePageInfo(cancellationToken);

            CourseSinglePageDto result = MappingCourseSingle(courseEntity);

            return new ApiResult<CourseSinglePageDto>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }

        //****************************MappingCourseSingle********************************
        private CourseSinglePageDto MappingCourseSingle(Course courseEntity)
        {
            var result = new CourseSinglePageDto();

            result.CreateDate = courseEntity.CreateDate;
            result.ShowCreateDate = courseEntity.CreateDate.ToShamsi();
            result.UpdateDate = courseEntity.UpdateDate;
            result.ShowUpdateDate = courseEntity.UpdateDate.ToShamsi();
            result.TeacherId = courseEntity.TeacherId;
            result.TeacherName = courseEntity.User.FirstName + " " + courseEntity.User.LastName;
            result.TotalEpisodeFileCount = courseEntity.CourseEpisodes.Count;
            result.StatusId = courseEntity.CourseStatusId;
            result.StatusTitle = courseEntity.CourseStatus.Title;
            result.LevelId = courseEntity.CourseLevelId;
            result.LevelTitle = courseEntity.CourseLevel.Title;
            result.CoursePrice = courseEntity.CoursePrice;
            result.CourseId = courseEntity.Id;
            result.CourseGroupId = courseEntity.CourseGroupId;
            result.CourseGroupTitle = courseEntity.CourseGroup.CourseGroupTitle;
            result.SubCourseGroupId = courseEntity.SubCourseGroupId;
            result.SubCourseGroupTitle = courseEntity.SubCourseGroup?.CourseGroupTitle;

            foreach (var courseEpisode in courseEntity.CourseEpisodes)
            {
                result.Episodes.Add(new CourseEpisodeForSinglePageDto
                {
                    EpisodeFileName = courseEpisode.EpisodeFileName,
                    EpisodeFileTitle = courseEpisode.EpisodeFileTitle,
                    EpisodeTime = courseEpisode.EpisodeTime,
                    IsFree = courseEpisode.IsFree,
                    EpisodeTimeSpan = TimeSpan.Parse(courseEpisode.EpisodeTime),
                });
            }

            foreach (var courseComment in courseEntity.CourseComments)
            {
                result.Comments.Add(new CourseCommentForSinglePageDto
                {
                    CourseId = courseComment.CourseId,
                    FirstName = courseComment.User.FirstName,
                    LastName = courseComment.User.LastName,
                    Text = courseComment.Text,
                    UserId = courseComment.User.Id,
                });
            }

            return result;
        }

        //******************************** SearchCourseByTitle *****************************
        public async Task<ApiResult<List<FilterCourseDto>>> SearchCourseByTitle(string courseTitle, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(courseTitle) && courseTitle.Length < 4)
                throw new AppException("تعداد کاراکتر وارد شده باید بیشتر باشد");

            var result = new List<FilterCourseDto>();
            var courseList = await _courseRepository.GetAllCourse(courseTitle, cancellationToken);

            foreach (var item in courseList)
            {
                result.Add(new FilterCourseDto
                {
                    CourseTitle = item.CourseTitle,
                    CoursePrice = item.CoursePrice,
                    IsFreeCost = item.IsFreeCost,
                    CourseImageBase64 = item.CourseImageBase64,
                    CourseImageName = item.CourseImageName,
                    CreatDateDisplay = item.CreateDate.ToShamsi()
                });
            }
            return new ApiResult<List<FilterCourseDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد");
        }

        //******************************** GetLastCourses ***********************************
        public async Task<ApiResult<List<FilterCourseDto>>> GetLastCourses(CancellationToken cancellationToken)
        {
            var result = new List<FilterCourseDto>();
            var courseList = await _courseRepository.GetLastCourses(cancellationToken);

            foreach (var item in courseList)
            {
                result.Add(new FilterCourseDto
                {
                    CourseTitle = item.CourseTitle,
                    CoursePrice = item.CoursePrice,
                    IsFreeCost = item.IsFreeCost,
                    CourseImageBase64 = item.CourseImageBase64,
                    CourseImageName = item.CourseImageName,
                    CreatDateDisplay = item.CreateDate.ToShamsi(),
                });
            }

            if (result is null)
                throw new AppException("دوره ای یافت نشد");

            return new ApiResult<List<FilterCourseDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }

        //******************************** GetPopularCourses ********************************
        public async Task<ApiResult<List<FilterCourseDto>>> GetPopularCourses(CancellationToken cancellationToken)
        {
            var result = new List<FilterCourseDto>();
            var courseList = await _courseRepository.GetPopularCourses(cancellationToken);

            foreach (var item in courseList)
            {
                result.Add(new FilterCourseDto
                {
                    CourseTitle = item.CourseTitle,
                    CoursePrice = item.CoursePrice,
                    IsFreeCost = item.IsFreeCost,
                    CourseImageBase64 = item.CourseImageBase64,
                    CourseImageName = item.CourseImageName,
                    CreatDateDisplay = item.CreateDate.ToShamsi(),
                });
            }

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

            try
            {
                var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > default(int);
                return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد");
            }
            catch (Exception)
            {
                _fileManagerService.DeleteFile(FileTypeEnum.CourseVideo, entity.DemoVideo, entity.CourseTitle);
                _fileManagerService.DeleteFile(FileTypeEnum.CourseImage, entity.CourseImageName, entity.CourseTitle);
                throw;
            }
        }

        //******************************** MappingCourseEntity ******************************
        private Course MappingCourseEntity(AddCourseDto request)
        {
            var saveImageResult = _fileManagerService.SaveImage(FileTypeEnum.CourseImage, request.FormFile, request.CourseTitle);
            var demoVideoName = _fileManagerService.SaveFile(FileTypeEnum.DemoCourseVideo, request.DemoFile, request.CourseTitle);

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

        //************************************* EditCourse **********************************
        public async Task<ApiResult> EditCourse(EditCourseDto request, CancellationToken cancellationToken)
        {
            //ToDo validation
            //ToDo business validation

            var entity = MappingCourseEorEdit(request);
            _courseRepository.Update(entity);

            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > default(int);

            if (!result)
                throw new AppException("عملیات ویرایش در دیتابیس با خطا مواجه شد");

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //************************************* MappingCourseEorEdit *************************
        private Course MappingCourseEorEdit(EditCourseDto request)
        {
            var course = _courseRepository.GetById(request.Id);
            var saveImageResult = _fileManagerService.SaveImage(FileTypeEnum.CourseImage, request.FormFile, course.CourseTitle, course.CourseImageName);
            var demoVideoName = _fileManagerService.SaveFile(FileTypeEnum.DemoCourseVideo, request.FormFile, request.DirectoryName);

            course.CourseGroupId = request.CourseGroupId;
            course.SubCourseGroupId = request.SubCourseGroupId;
            course.CourseLevelId = request.CourseLevelId;
            course.CourseStatusId = request.CourseStatusId;
            course.TeacherId = request.TeacherId;
            course.CourseTitle = request.CourseTitle;
            course.CoursePrice = request.CoursePrice;
            course.IsFreeCost = request.IsFreeCost;
            course.CourseImageBase64 = saveImageResult.AvatarBase64;
            course.CourseImageName = saveImageResult.AvatarName;
            course.DemoVideo = demoVideoName;

            return course;

        }

        //************************************* Delete ***************************************
        public async Task<ApiResult> Delete(int courseId, CancellationToken cancellationToken)
        {

            using (var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                await DeleteCourse(courseId, cancellationToken);
                await _courseEpisodeService.DeleteCourseEpisodeByCourseId(courseId, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //************************************* DeleteCourse *********************************
        private async Task DeleteCourse(int courseId, CancellationToken cancellationToken)
        {
            var entity = await _courseRepository.GetByIdAsync(courseId, cancellationToken);
            if (entity is null)
                throw new AppException("موردی یافت نشد");

            DeleteFiles(entity);
            _courseRepository.LogicalDelete(entity.Id);
        }

        //************************************* DeleteFiles **********************************
        private void DeleteFiles(Course? entity)
        {
            _fileManagerService.DeleteFile(FileTypeEnum.DemoCourseVideo, entity.DemoVideo, entity.CourseTitle);
            _fileManagerService.DeleteFile(FileTypeEnum.CourseImage, entity.CourseImageName, entity.CourseTitle);
        }

        //************************************* GetFilterArchivedCourses *********************
        public async Task<DataGridResult<FilterArchivedCoursesResponseDto>> GetFilterArchivedCourses(FilterArchivedCoursesRequestDto request, CancellationToken cancellationTokenationToken)
        {
            var result = new DataGridResult<FilterArchivedCoursesResponseDto>();
            var courseEntity = await Filter(request);

            result.Total = courseEntity.Total;
            result.Data = courseEntity.Data
                .Select(x => new FilterArchivedCoursesResponseDto
                {
                    CourseTitle = x.CourseTitle,
                    CourseImageBase64 = x.CourseImageBase64,
                    CourseImageName = x.CourseImageName,
                    CourseLevelId = x.CourseLevelId,
                    CourseStatusId = x.CourseStatusId,
                    IsFreeCost = x.IsFreeCost,
                    TeacherId = x.TeacherId,
                    ViewCount = x.ViewCount

                }).ToList();

            return result;
        }

        //************************************* Filter **************************************
        private async Task<DataGridResult<Course>> Filter(FilterArchivedCoursesRequestDto request)
        {
            var expresion = _courseRepository
                .FetchIQueryableEntity()
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.CourseTitle))
                expresion = expresion.Where(x => x.CourseTitle.Contains(request.CourseTitle));

            if (request.CourseGroupIds is not null && !request.CourseGroupIds.Any())
                expresion = expresion.Where(x => request.CourseGroupIds.Contains(x.CourseGroupId));

            if (request.SubCourseGroupIds is not null && !request.SubCourseGroupIds.Any())
                expresion = expresion.Where(x => x.SubCourseGroupId.HasValue && request.SubCourseGroupIds.Contains(x.SubCourseGroupId.Value));

            if (request.IsFreeCost is true)
                expresion = expresion.Where(x => x.IsFreeCost == request.IsFreeCost);

            if (request.StartPrice.HasValue && request.StartPrice != 0)
                expresion = expresion.Where(x => x.CoursePrice >= request.StartPrice);

            if (request.EndPrice.HasValue && request.EndPrice != 0)
                expresion = expresion.Where(x => x.CoursePrice <= request.EndPrice);

            if (request.OrderByType == OrderByEnum.MinPrice)
                expresion = expresion.OrderBy(x => x.CoursePrice);

            if (request.OrderByType == OrderByEnum.MaxPrice)
                expresion = expresion.OrderByDescending(x => x.CoursePrice);

            var filter = await expresion
                .ApplyPaging(new GridState
                {
                    Take = request.Take,
                    Skip = request.Skip,
                    Dir = request.Dir
                });

            return filter;
        }
    }
}