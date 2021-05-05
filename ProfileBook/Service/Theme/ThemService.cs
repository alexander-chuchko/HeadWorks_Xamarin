using ProfileBook.Enum;
using ProfileBook.Service.Settings;
using ProfileBook.Styles;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProfileBook.Service.Theme
{
    public class ThemService :IThemService
    {
        private ISettingsManager _settingsManager;
        public ThemService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
        public bool GetValueDarkTheme()
        {
            return _settingsManager.IsDarkTheme;
        }
        public void SetValueValueDarkTheme(bool value)
        {
            _settingsManager.IsDarkTheme = value;
        }
        public void RemoveThemeDark()
        {
            _settingsManager.RemoveDarkTheme();
        }
        public void SetDefaultTheme()
        {
            RemoveThemeDark();
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(new LightTheme());
            }
        }
    }
}
