using CompanyMVC.Service;
using CompanyMVC.Domain.Repositories.Abstract;
using CompanyMVC.Domain.Repositories.EntityFramework;
using CompanyMVC.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//позволяет как получить доступ, так и настроить конфигурацию
//ConfigurationManager configuration = builder.Configuration;

IServiceCollection services = builder.Services;
IWebHostEnvironment environment = builder.Environment;
IConfiguration configuration = builder.Configuration;

// связать Project с config файлом
configuration.Bind("Project", new Config());

//связь интерфейса с его реализацией, делаем транзиентными, т.е. в рамках одного http запроса может быть
//создано сколько угодно объектов этих репозиторией
services.AddTransient<ITextFieldRepository, EFTextFieldRepository>();
services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
services.AddTransient<DataManager>();

//подключение контекст БД, тут ConnectionString берется из appsettings.json
services.AddDbContext<AddDbContext>(x => x.UseSqlServer(Config.ConnectionString));

//настраиваем identity систему
services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
}).AddEntityFrameworkStores<AddDbContext>().AddDefaultTokenProviders();

services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "companyAuth";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
});

services.AddControllersWithViews().AddSessionStateTempDataProvider();

WebApplication app = builder.Build();
//в процессе создания сайта, такие ошибки возникают
if (environment.IsDevelopment())
{
    // использовать страницы исклчючений для разработчиков
    app.UseDeveloperExceptionPage();
}

// подключается система маршрутизациии
//app.UseRouting();

// подключаем поддержку статических файлов в приложении (css, js ...)
app.UseStaticFiles();

//подключаем аутенфикацию и авторизацию
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
//app.MapGet("/", () => "Hello World!");

app.Run();
