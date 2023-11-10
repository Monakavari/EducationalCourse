using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Common.Enums;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.CustomException;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class CourseEpisodeService : ICourseEpisodeService
    {
        #region Constructor

        private readonly IFileManagerService _fileManagerService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseEpisodeRepository _episodeRepository;
        public CourseEpisodeService(IFileManagerService fileManagerService,
                                    IUnitOfWork unitOfWork,
                                    ICourseEpisodeRepository episodeRepository)
        {
            _fileManagerService = fileManagerService;
            _unitOfWork = unitOfWork;
            _episodeRepository = episodeRepository;
        }

        #endregion Constructor

        //************************************* CreateCourseEpisode *************************
        public async Task<ApiResult> CreateCourseEpisode(List<AddVideoFileCourseDto> request, CancellationToken cancellationToken)
        {
            var entities = MappingCourseEpisode(request);
            await _episodeRepository.AddRangeAsync(entities, cancellationToken);

            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد");
            }
            catch (Exception)
            {
                foreach (var item in entities)
                    _fileManagerService
                            .DeleteFile(FileTypeEnum.CourseVideo, item.EpisodeFileName, request.First().CourseTitle);

                throw;
            }
        }

        //************************************* MappingCourseEpisode *************************
        private List<CourseEpisode> MappingCourseEpisode(List<AddVideoFileCourseDto> request)
        {
            var result = new List<CourseEpisode>();

            foreach (var item in request)
            {
                var fileName = _fileManagerService.SaveFile(FileTypeEnum.CourseVideo, item.VideoFiles, request.First().CourseTitle);
                result.Add(new CourseEpisode
                {
                    CourseId = item.CourseId,
                    EpisodeTime = item.EpisodeTime,
                    IsFree = item.IsFree,
                    IsActive = true,
                    EpisodeFileName = fileName,
                    EpisodeFileTitle = item.EpisodeFileTitle
                });
            }

            return result;
        }

        //************************************* GetAllCourseEpisodeByCourseId ****************
        public async Task<ApiResult<List<CourseEpisodeDto>>> GetAllCourseEpisodeByCourseId(int courseId, CancellationToken cancellationToken)
        {
            var result = await _episodeRepository.GetAllCourseEpisodeByCourseId(courseId, cancellationToken);

            if (result is null)
                throw new AppException("موردی یافت نشد.");

            return new ApiResult<List<CourseEpisodeDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }

        //************************************* DeleteVideoFileFromCourse ********************
        public async Task<ApiResult> DeleteCourseEpisode(int courseEpisodeId, CancellationToken cancellationToken)
        {
            //TODo Validations
            //TODo business
            var entity = await _episodeRepository.GetById(courseEpisodeId, cancellationToken);
            if (entity is null)
                throw new AppException("موردی یافت نشد");

            _fileManagerService.DeleteFile(FileTypeEnum.CourseVideo, entity.EpisodeFileName, entity.Course.CourseTitle);
            _episodeRepository.Delete(courseEpisodeId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد");
        }

        //************************************* DeleteCourseEpisodeByCourseId ****************
        public async Task DeleteCourseEpisodeByCourseId(int courseId, CancellationToken cancellationToken)
        {
            var courseEpisodList = await _episodeRepository
                      .FetchIQueryableEntity()
                      .Include(x => x.CourseId)
                      .Where(x => x.CourseId == courseId)
                      .ToListAsync(cancellationToken);

            DeleteFiles(courseEpisodList);
            _episodeRepository.DeleteRange(courseEpisodList.Select(c => c.Id).ToList());
        }

        //************************************* DeleteFiles **********************************
        private void DeleteFiles(List<CourseEpisode> courseEpisodList)
        {
            foreach (var item in courseEpisodList)
                _fileManagerService.DeleteFile(FileTypeEnum.CourseVideo, item.EpisodeFileName, item.Course.CourseTitle);
        }

        //************************************* EditCourseEpisode ****************************
        public async Task<ApiResult> EditCourseEpisode(List<EditCourseEpisodeDto> request, CancellationToken cancellationToken)
        {
            var courseEpisodeList = await _episodeRepository.GetAllCourseEpisode(request.First().CourseId, cancellationToken);

            await CreateNew(request, cancellationToken);
            Edit(courseEpisodeList, request);
            Delete(courseEpisodeList, request);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //************************************* Delete ****************************************
        private void Delete(List<CourseEpisode> courseEpisodeList, List<EditCourseEpisodeDto> request)
        {
            var currentIdsList = courseEpisodeList.Select(c => c.Id).ToList();
            var incomeIdsList = request.Select(x => x.CourseEpisodeId).ToList();
            var DeletedIdsList = currentIdsList.Except(incomeIdsList).ToList();
            _episodeRepository.DeleteRange(DeletedIdsList);

            DeleteFiles(courseEpisodeList.Where(x => DeletedIdsList.Contains(x.Id)).ToList());
        }

        //************************************* Edit ****************************************
        private void Edit(List<CourseEpisode> courseEpisodeList, List<EditCourseEpisodeDto> request)
        {
            foreach (var item in request.Where(x => x.CourseEpisodeId > 0))
            {
                var currentCourseEpisode = courseEpisodeList.Where(x => x.Id == item.CourseEpisodeId).FirstOrDefault();

                if (currentCourseEpisode is not null)
                {
                    currentCourseEpisode.EpisodeFileName = item.EpisodeFileName;
                    currentCourseEpisode.EpisodeFileTitle = item.EpisodeFileTitle;
                    currentCourseEpisode.IsFree = item.IsFree;
                    currentCourseEpisode.EpisodeTime = item.EpisodeTime;

                    _episodeRepository.Update(currentCourseEpisode);
                }
            }
        }

        //************************************* CreateNew ************************************
        private async Task CreateNew(List<EditCourseEpisodeDto> request, CancellationToken cancellationToken)
        {
            var entities = new List<CourseEpisode>();

            foreach (var item in request.Where(x => x.CourseEpisodeId == 0))

                entities.Add(new CourseEpisode
                {
                    CourseId = item.CourseId,
                });

            await _episodeRepository.AddRangeAsync(entities, cancellationToken);
        }

    }
}
