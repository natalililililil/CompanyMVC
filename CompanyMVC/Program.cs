WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//��������� ��� �������� ������, ��� � ��������� ������������
//ConfigurationManager configuration = builder.Configuration;

IServiceCollection services = builder.Services;
services.AddControllersWithViews().AddSessionStateTempDataProvider();

WebApplication app = builder.Build();
IWebHostEnvironment environment = builder.Environment;

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

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
//app.MapGet("/", () => "Hello World!");

app.Run();
