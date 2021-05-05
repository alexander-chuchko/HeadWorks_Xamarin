using ProfileBook.Service.Settings;

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
    }
}
