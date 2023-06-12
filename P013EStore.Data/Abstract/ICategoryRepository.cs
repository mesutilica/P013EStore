using P013EStore.Core.Entities;

namespace P013EStore.Data.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetCategoryByIncludeAsync(int id);
    }
}
