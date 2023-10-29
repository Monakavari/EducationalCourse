using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Framework.BaseSearching
{
    public interface IBaseSearchable<TModel, TKey, TSearchResult, TSearchModel>
    {
        Task<List<TSearchResult>> Search(TSearchModel sm, out int recordCount);
    }
}
