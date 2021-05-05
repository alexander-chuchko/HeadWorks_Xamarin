using ProfileBook.Resource;
using ProfileBook.Service.Settings;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace ProfileBook.Service.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private ISettingsManager _settingsManager;


        public LocalizationService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
        public void SetValueLanguage(string value)
        {
            _settingsManager.SelectedLanguage = value;
        }
        public string GetValueLanguage()
        {
            return _settingsManager.SelectedLanguage;
        }
        public void RemoveLanguage()
        {
            _settingsManager.RemoveLanguage();
        }
        public void SetDefaultLanguage()
        {
            if ("English" != GetValueLanguage())
            {
                RemoveLanguage();
                var result = CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToList().First(x => x.NativeName.Contains("English"));
                CultureInfo language = new CultureInfo(result.Name);
                Thread.CurrentThread.CurrentUICulture = language;
                AppResource.Culture = language;
            }
        }
    }
}
