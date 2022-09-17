using CompanyMVC.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyMVC.Domain
{
    public class AddDbContext : IdentityDbContext<IdentityUser>
    {
        public AddDbContext(DbContextOptions<AddDbContext> options) : base (options) { }
        //проецирует на базу данных эти два класса, в базе будут созданы
        //таблицы с этими именами
        public DbSet<TextField> TextFields { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //заполнить бд значениями по умолчанию
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "E7379CA3-97B5-4C68-BF36-0000476A75D3",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            //на момент создания один пользователь - админ
            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "711FB9A8-1C86-42D8-9C17-DD4BB6843A9D",
                UserName = "admin",
                NormalizedUserName = "Admin",
                Email = "my@email.com",
                NormalizedEmail = "MY@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "syperpassword"),
                SecurityStamp = string.Empty
            });

            //промежуточная таблица
            //связываем админа с его ролью
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "E7379CA3-97B5-4C68-BF36-0000476A75D3",
                UserId = "711FB9A8-1C86-42D8-9C17-DD4BB6843A9D"
            });

            //3 объекта в БД, текстовые поля
            builder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("A71B0942-9AAC-4FA5-A4CA-4196A4B78E5E"),
                CodeWord = "PageIndex",
                Title = "Главная"
            });

            builder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("4DFD7E44-0B87-41B0-A30F-1D2244844913"),
                CodeWord = "PageServices",
                Title = "Главная"
            });

            builder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("CE13AB04-DDF4-4FB4-9568-693E56DD9C9A"),
                CodeWord = "PageContacts",
                Title = "Контакты"
            });
        }
    }
}
