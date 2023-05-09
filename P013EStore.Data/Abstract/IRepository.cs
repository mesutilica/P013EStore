using System.Linq.Expressions; // kendi lambda Expression(x=>x.) kullanabileceğimiz metotları yazmamızı sağlayan kütüphane.

namespace P013EStore.Data.Abstract
{
    public interface IRepository<T> where T : class // IRepository interface i dışarıdan alacağı T tipinde bir parametreyle çalışacak ve where şartı ile bu T nin veri tipi bir class olmalıdır dedik.
    {
        // Senkron Metotlar
        List<T> GetAll(); // db deki tüm kayıtları çekmemizi sağlayacak metot imzası
        List<T> GetAll(Expression<Func<T, bool>> expression); // expression uygulamada verileri listelerken p=>p.IsActive vb gibi sorgulama ve filtreleme kodları kullanabilmemizi sağlar
        T Get(Expression<Func<T, bool>> expression);
        T Find(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Save();
        // Asenkron Metotlar
        Task<T> FindAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression); // lambda expression kullanarak db de filtrleme yapıp geriye 1 tane kayıt döndürür
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression); // lambda expression kullanarak db de filtrleme yapıp geriye liste döndürür
        Task AddAsync(T entity);
        Task<int> SaveAsync(); // asenkron kaydetme
    }
}
