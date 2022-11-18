using CompanyMVC.Domain.Entities;
using CompanyMVC.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CompanyMVC.Domain.Repositories.EntityFramework
{
    public class EFServiceItemsRepository : IServiceItemsRepository
    {
        private readonly AddDbContext context;
        public EFServiceItemsRepository(AddDbContext context)
        {
            this.context = context;
        }

        //выбираем все записи из бд
        public IQueryable<ServiceItem> GetServiceItems()
        {
            return context.ServiceItems;
        }
        //выбираем записи по ид
        public ServiceItem GetServiceItemById(Guid id)
        {
            return context.ServiceItems.FirstOrDefault(x => x.Id == id);
        }

        public void SaveServiceItem(ServiceItem entity)
        {
            if (entity.Id == Guid.Empty)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteServiceItem(Guid id)
        {
            context.ServiceItems.Remove(new ServiceItem() { Id = id });
            context.SaveChanges();
        }
    }
}
