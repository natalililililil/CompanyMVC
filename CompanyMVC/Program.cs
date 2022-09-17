using CompanyMVC.Service;
using CompanyMVC.Domain.Repositories.Abstract;
using CompanyMVC.Domain.Repositories.EntityFramework;
using CompanyMVC.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//��������� ��� �������� ������, ��� � ��������� ������������
//ConfigurationManager configuration = builder.Configuration;

IServiceCollection services = builder.Services;
IWebHostEnvironment environment = builder.Environment;
IConfiguration configuration = builder.Configuration;

// ������� Project � config ������
configuration.Bind("Project", new Config());

//����� ���������� � ��� �����������, ������ �������������, �.�. � ������ ������ http ������� ����� ����
//������� ������� ������ �������� ���� ������������
services.AddTransient<ITextFieldRepository, EFTextFieldRepository>();
services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
services.AddTransient<DataManager>();

//����������� �������� ��, ��� ConnectionString ������� �� appsettings.json
services.AddDbContext<AddDbContext>(x => x.UseSqlServer(Config.ConnectionString));

//����������� identity �������
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
//� �������� �������� �����, ����� ������ ���������
if (environment.IsDevelopment())
{
    // ������������ �������� ����������� ��� �������������
    app.UseDeveloperExceptionPage();
}

// ������������ ������� ��������������
//app.UseRouting();

// ���������� ��������� ����������� ������ � ���������� (css, js ...)
app.UseStaticFiles();

//���������� ������������ � �����������
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
//app.MapGet("/", () => "Hello World!");

app.Run();
