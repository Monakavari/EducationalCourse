using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class CourseEpisodeRepository : BaseRepository<CourseEpisode>, ICourseEpisodeRepository
    {

        #region Constructor

        private readonly EducationalCourseContext _context;
        public CourseEpisodeRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        //**********************************GetAllCourseEpisodeByCourseId**********************
        public async Task<List<CourseEpisodeDto>> GetAllCourseEpisodeByCourseId(int courseId, CancellationToken cancellationToken)
        {
            var result = await _context.CourseEpisodes
                               .Where(x => x.Id == courseId)
                               .Select(x => new CourseEpisodeDto
                               {
                                   EpisodeTime = x.EpisodeTime,
                                   EpisodeFileName = x.EpisodeFileName,
                                   EpisodeFileTitle = x.EpisodeFileTitle,
                                   IsFree = x.IsFree

                               }).ToListAsync(cancellationToken);

            return result;
        }

        //**********************************GetAllCourseEpisode********************************
        public async Task<List<CourseEpisode>> GetAllCourseEpisode(int courseId, CancellationToken cancellationToken)
        {
            return await _context.CourseEpisodes
                               .Where(x => x.Id == courseId)
                              .ToListAsync(cancellationToken);
                        
        }

        //**********************************GetById********************************************
        public async Task<CourseEpisode> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.CourseEpisodes
                                .Include(x => x.Course)
                                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

    }
}
