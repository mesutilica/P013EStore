using System;
using System.Collections.Generic;
using System.Linq.Expressions; // kendi lambda Expression(x=>x.) kullanabileceğimiz metotları yazmamızı sağlayan kütüphane.

namespace P013EStore.Data.Abstract
{
    public interface IRepository<T> where T : class // IRepository interface i dışarıdan alacağı T tipinde bir parametreyle çalışacak ve where şartı ile bu T nin veri tipi bir class olmalıdır dedik.
    {
        List<T> GetAll(); // db deki tüm kayıtları çekmemizi sağlayacak metot imzası
        List<T> GetAll(Expression<Func<T, bool>> expression); // expression uygulamada verileri listelerken p=>p.IsActive vb gibi sorgulama ve filtreleme kodları kullanabilmemizi sağlar

    }
}
