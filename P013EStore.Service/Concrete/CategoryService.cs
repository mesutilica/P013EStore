using P013EStore.Data;
using P013EStore.Data.Concrete;
using P013EStore.Service.Abstract;

namespace P013EStore.Service.Concrete
{
    public class CategoryService : CategoryRepository, ICategoryService
    {
        public CategoryService(DatabaseContext context) : base(context)
        {
        }
    }
}
