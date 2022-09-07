WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//позволяет как получить доступ, так и настроить конфигурацию
//ConfigurationManager configuration = builder.Configuration;

IServiceCollection services = builder.Services;
services.AddControllersWithViews().AddSessionStateTempDataProvider();

WebApplication app = builder.Build();
IWebHostEnvironment environment = builder.Environment;

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

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
//app.MapGet("/", () => "Hello World!");

app.Run();
