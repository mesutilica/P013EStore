using P013EStore.Data;
using P013EStore.Data.Concrete;
using P013EStore.Service.Abstract;

namespace P013EStore.Service.Concrete
{
    public class ProductService : ProductRepository, IProductService
    {
        public ProductService(DatabaseContext context) : base(context)
        {
        }
    }
}
