using P013EStore.Core.Entities;
using P013EStore.Data.Abstract;

namespace P013EStore.Service.Abstract
{
    public interface IService<T> : IRepository<T> where T : class, IEntity, new()
    {
    }
}
