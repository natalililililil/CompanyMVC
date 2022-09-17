using CompanyMVC.Domain.Entities;

namespace CompanyMVC.Domain.Repositories.Abstract
{
    public interface ITextFieldRepository
    {
        //сделать выборку всех текстовых полей
        IQueryable<TextField> GetTextFields();

        //выбрать текстовое поле по id
        TextField GetTextFieldByID(Guid id);
        TextField GetTextFieldByCodeWord(string codeWord);
        void SaveTextField(TextField entity);
        void DeleteTextField(Guid id);
    }
}
