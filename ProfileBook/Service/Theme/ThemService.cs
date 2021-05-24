using ProfileBook.Enum;
using ProfileBook.Service.Settings;
using ProfileBook.Styles;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProfileBook.Service.Theme
{
    public class ThemService :IThemService
    {
        private readonly ISettingsManager _settingsManager;
        public ThemService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
        public EnumSet.Theme GetValueTheme()
        {
            return (EnumSet.Theme)_settingsManager.ThemType;
        }
        public void SetValueTheme(EnumSet.Theme themType)
        {
            _settingsManager.ThemType = (int)themType;
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
