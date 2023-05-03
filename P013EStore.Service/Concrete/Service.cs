using P013EStore.Core.Entities;
using P013EStore.Data;
using P013EStore.Data.Concrete;
using P013EStore.Service.Abstract;

namespace P013EStore.Service.Concrete
{
    public class Service<T> : Repository<T>, IService<T> where T : class, IEntity, new()
    {
        public Service(DatabaseContext context) : base(context)
        {
        }
    }
}
