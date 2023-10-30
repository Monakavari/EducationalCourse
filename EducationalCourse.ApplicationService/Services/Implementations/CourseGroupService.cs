using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Models.Course;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<ApiResult> AddCourseGroup()
        {
            throw new NotImplementedException();
        }

        #endregion Constructor

        //*************************************GetAllCourseGroups*****************************************
        public async Task<ApiResult<List<CourseGroupDto>>> GetAllCourseGroups(CancellationToken cancellationToken)

        {
            var result = new List<CourseGroupDto>();
            var entity = await _courseGroupRepository.GetCourseGroups();

            if (entity == null)
                throw new Exception("گروهی یافت نشد");

            GetAllParents(result, entity);
            //GetAllChildren(result, entity);

            return new ApiResult<List<CourseGroupDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");

        }

        private void GetAllParents(List<CourseGroupDto> result, List<CourseGroup> entity)
        {
            foreach (var item in entity.Where(x => x.ParentId == null))
            {
                result.Add(new CourseGroupDto
                {
                    id = item.Id,
                    Title = item.CourseGroupTitle,
                });
            }

        }

        //private void GetAllChildren(List<CourseGroupDto> result, List<CourseGroup> entity)
        //{
        //    var courseGroup = _courseGroupRepository.GetById();
        //    foreach (var item in entity.Where(x => x.Id= ))
        //}
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