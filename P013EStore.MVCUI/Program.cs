using P013EStore.Data;
using P013EStore.Service.Abstract;
using P013EStore.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies; // Oturum iþlemleri için

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(); // uygulamada session kullanabilmek için

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); // kendi yazdýðýmýz db iþlemlerini yapan servisi .net core da bu þekilde mvc projesine servis olarak tanýtýyoruz ki kullanabilelim.
builder.Services.AddTransient<IProductService, ProductService>(); // Product için yazdýðýmýz özel servisi uygulamaya tanýttýk

// Uygulama admin paneli için oturum açma ayarlarý
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login"; // oturum açmayan kullanýcýlarýn giriþ için gönderileceði adres
    x.LogoutPath = "/Admin/Logout";
    x.AccessDeniedPath = "/AccessDenied"; // yetkilendirme ile ekrana eriþim hakký olmayan kullanýcýlarýn gönderileceði sayfa
    x.Cookie.Name = "Administrator"; // Oluþacak kukinin ismi
    x.Cookie.MaxAge = TimeSpan.FromDays(1); // Oluþacak kukinin yaþam süresi(1 gün)
}); // Oturum iþlemleri için

// Uygulama admin paneli için admin yetkilendirme ayarlarý
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin")); // admin paneline giriþ yapma yetkisine sahip olanlarý bu kuralla kontrol edeceðiz
    x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User")); // admin dýþýnda yetkilendirme kullanýrsak bu kuralý kullanabiliriz(siteye üye giriþi yapanlarý ön yüzde bir panele eriþtirme gibi)
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // session için

app.UseAuthentication(); // Dikkat! önce UseAuthentication satýrý gelmeli sonra UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
