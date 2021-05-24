
namespace ProfileBook.Service.Localization
{
    public interface ILocalizationService
    {
        string GetValueLanguage();
        void SetValueLanguage(string value);
        void ChangeApplicationLanguage(string selectedLanguage);
    }
}
