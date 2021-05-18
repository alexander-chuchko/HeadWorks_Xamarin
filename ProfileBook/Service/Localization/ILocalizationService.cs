
namespace ProfileBook.Service.Localization
{
    public interface ILocalizationService
    {
        string GetValueLanguage();
        void SetValueLanguage(string value);
        void SetDefaultLanguage();
        void ChangeApplicationLanguage(string selectedLanguage);
    }
}
