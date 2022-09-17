using CompanyMVC.Domain.Repositories.Abstract;

namespace CompanyMVC.Domain
{
    public class DataManager
    {
        public ITextFieldRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }

        public DataManager(ITextFieldRepository textFieldRepository, IServiceItemsRepository serviceItemsRepository)
        {
            TextFields = textFieldRepository;
            ServiceItems = serviceItemsRepository;
        }
    }
}
