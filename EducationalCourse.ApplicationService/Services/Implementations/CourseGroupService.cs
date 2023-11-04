using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.CustomException;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class CourseGroupService : ICourseGroupService
    {
        #region Constructor

        private readonly ICourseGroupRepository _courseGroupRepository;

        public CourseGroupService(ICourseGroupRepository courseGroupRepository)
        {
            _courseGroupRepository = courseGroupRepository;
        }

        #endregion Constructor

        //************************************* LoadCourseGroupParentChaild *************************
        public async Task<ApiResult<List<CourseGroupDto>>> GetAllCourseGroups(CancellationToken cancellationToken)
        {
            var courseGroupList = await _courseGroupRepository.FetchIQueryableEntity().Where(x => x.IsActive).ToListAsync(cancellationToken);
            var result = new List<CourseGroupDto>();

            if (courseGroupList is null)
                throw new Exception("گروهی یافت نشد ");

            result = LoadCourseGroupParentChild(result, courseGroupList);

            return new ApiResult<List<CourseGroupDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }

        //************************************* LoadCourseGroupParentChaild *************************
        private List<CourseGroupDto> LoadCourseGroupParentChild(List<CourseGroupDto> request, List<CourseGroup> courseGroupList)
        {
            foreach (var courseGroup in courseGroupList.Where(x => x.ParentId is null))
            {
                request.Add(new CourseGroupDto
                {
                    id = courseGroup.Id,
                    Title = courseGroup.CourseGroupTitle,
                    ParentId = courseGroup.ParentId,
                    Children = LoadChildren(courseGroupList, courseGroup.Id)
                });
            }
            return request;
        }

        //************************************* LoadChildren ****************************************
        private List<CourseGroupDto> LoadChildren(List<CourseGroup> courseGroupList, int parentId)
        {
            var children = new List<CourseGroupDto>();

            foreach (var courseGroup in courseGroupList.Where(x => x.ParentId == parentId))
            {
                children.Add(new CourseGroupDto
                {
                    id = courseGroup.Id,
                    Title = courseGroup.CourseGroupTitle,
                    ParentId = courseGroup.ParentId,
                    Children = LoadChildren(courseGroupList, courseGroup.Id)
                });
            }
            return children;
        }

        //************************************* AddParent *******************************************
        public async Task<ApiResult> AddParent(AddParentCourseGroupDto request, CancellationToken cancellationToken)
        {
            if (request.CourseGroupTitle is null)
                throw new AppException("عنوان نمی تواند خالی باشد");

            if (await _courseGroupRepository.ExistCourseGroupName(request.CourseGroupTitle, cancellationToken))
            {
                throw new AppException("عنوان نمی تواند تکراری باشد");

            }
            CourseGroup courseGroup = new CourseGroup
            {
                CourseGroupTitle = request.CourseGroupTitle,
                ParentId = null,
            };

            await _courseGroupRepository.AddAsync(courseGroup, cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //************************************* AddChild ********************************************
        public async Task<ApiResult> AddChild(AddChildCourseGroupDto request, CancellationToken cancellationToken)
        {
            if (request.CourseGroupTitle is null)
                throw new AppException("عنوان نمی تواند خالی باشد");

            if (await _courseGroupRepository.ExistCourseGroupName(request.CourseGroupTitle, cancellationToken))
            {
                throw new AppException("عنوان نمی تواند خالی باشد");

            }

            CourseGroup courseGroup = new CourseGroup
            {
                CourseGroupTitle = request.CourseGroupTitle,
                Id = request.ParentId,
            };

            await _courseGroupRepository.AddAsync(courseGroup, cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }
    }

}
//public List<CategoryItemList> Search(CategorySearchModel sm, out int RecordCount)
//{
//    if (sm.PageSize == 0)
//        sm.PageSize = 10;

//    var q = from cat in db.Categories select cat;
//    if (!string.IsNullOrEmpty(sm.CategoryName))
//        q = q.Where(x => x.CategoryName.StartsWith(sm.CategoryName));

//    if (sm.ParentId != null)
//        q = q.Where(x => x.ParentID == sm.ParentId);

//    RecordCount = q.Count();
//    return q.Select(x => new CategoryItemList
//    {
//        CategoryID = x.CategoryID,
//        CategoryName = x.CategoryName,
//        ParentCount = x.ParentCount,
//        SortOrder = x.SortOrder,

//    }

//    ).OrderByDescending(x => x.SortOrder).Skip(sm.PageIndex * sm.PageSize).Take(sm.PageSize).ToList();

//public OperationResult Register(CategoryAddEditModel current)
//{
//    Category cat = new Category
//    {
//        CategoryName = current.CategoryName,
//        ParentID = current.ParentID,
//        SortOrder = current.SortOrder,
//        DirectChildCount = 0,
//        ParentCount = 0
//    };

//    var result = _categoryRepository.Register(cat);

//    _categoryRepository.IncrementParentDirectChildCount(result.RecordID.Value);

//    var linage = _categoryRepository.GenerateLinage(result.RecordID.Value);

//    _categoryRepository.SetLinage(linage, result.RecordID.Value);


//    return result;
//}

//public OperationResult Remove(int categoryId)
//{
//    var cat = Get(categoryId);

//    if (_categoryRepository.HasRelatedProduct(categoryId))
//        return new OperationResult("Remove category").ToFail("Remove category was failed");

//    if (_categoryRepository.HasChildCategory(categoryId))
//        return new OperationResult("Remove category").ToFail("Remove category was failed");

//    _categoryRepository.DecrementParentDirectChildCount(categoryId);
//    _categoryRepository.RemoveCategoryFeature(categoryId);
//    var result = _categoryRepository.Remove(categoryId);

//    return result;
//}

//public OperationResult Update(CategoryAddEditModel current)
//{
//    if (_categoryRepository.ExistCategoryNameForAnotherCategory(current.CategoryName, current.CategoryID))
//    {
//        return new OperationResult("Update Category").ToFail("CategoryName already exists");

//    }
//    var c = new Category
//    {
//        CategoryID = current.CategoryID,
//        CategoryName = current.CategoryName,
//        Description = current.Description,
//        SortOrder = current.SortOrder,

//    };
//    var oldcat = _categoryRepository.Get(current.CategoryID);
//    if (oldcat.ParentID == current.ParentID)
//    {

//        return _categoryRepository.Update(c);
//    }
//    else
//    {

//        var op = _categoryRepository.Update(c);
//        bool s = false;
//        if (op.Success)
//        {
//            s = _categoryRepository.GenerateAndSetLineageOnUpdateParentIdForNodeAndItsChildren(current.CategoryID, current.ParentID);
//        }
//        if (s && op.Success)
//        {
//            return op;
//        }
//        else
//        {
//            return op.ToFail("Update Failed");
//        }
//    }