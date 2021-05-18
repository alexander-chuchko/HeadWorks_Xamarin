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
        public void SetValueDarkTheme(bool value)
        {
            _settingsManager.IsDarkTheme = value;
        }
        public void SetDefaultTheme()
        {
            //RemoveThemeDark();
            SetValueDarkTheme(false);
            PerformThemeChange(EnumSet.Theme.Light);
        }
        public void PerformThemeChange(EnumSet.Theme theme)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                switch (theme)
                {
                    case EnumSet.Theme.Dark:
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case EnumSet.Theme.Light:
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }
        }
    }
}
