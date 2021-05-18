using ProfileBook.Helpers;
using Xamarin.Essentials;

namespace ProfileBook.Service.Settings
{
    class SettingsManager : ISettingsManager
    {
        public int Id 
        {
            get 
            { 
                return Preferences.Get(nameof(Id), 0);
            }
            set 
            {
                Preferences.Set(nameof(Id), value);
            } 
        }
        public int SortingType
        {
            get 
            {
                return Preferences.Get(nameof(SortingType), 0);
            }
            set
            {
                Preferences.Set(nameof(SortingType), value);
            }
        }
        public bool IsDarkTheme
        {
            get
            {
                return Preferences.Get(nameof(IsDarkTheme), false);
            }
            set
            {
                Preferences.Set(nameof(IsDarkTheme), value);
            }
        }
        public string SelectedLanguage
        {
            get
            {
                return Preferences.Get(nameof(SelectedLanguage), ListOfNames.english);
            }
            set
            {
                Preferences.Set(nameof(SelectedLanguage), value);
            }
        }
    }
}
