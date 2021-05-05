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
        public bool IsSortByName
        {
            get
            {
                return Preferences.Get(nameof(IsSortByName), false);
            }
            set
            {
                Preferences.Set(nameof(IsSortByName), value);
            }
        }
        public bool IsSortByNickName
        {
            get
            {
                return Preferences.Get(nameof(IsSortByNickName), false);
            }
            set
            {
                Preferences.Set(nameof(IsSortByNickName), value);
            }
        }
        public bool IsSortByDateAddedToDatabase
        {
            get
            {
                return Preferences.Get(nameof(IsSortByDateAddedToDatabase), false);
            }
            set
            {
                Preferences.Set(nameof(IsSortByDateAddedToDatabase), value);
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
                return Preferences.Get(nameof(SelectedLanguage), "English");
            }
            set
            {
                Preferences.Set(nameof(SelectedLanguage), value);
            }
        }
        public void RemoveCurrentId()
        {
            Preferences.Remove(nameof(Id));
        }
        public void RemoveDarkTheme()
        {
            Preferences.Remove(nameof(IsDarkTheme));
        }
        public void RemoveLanguage()
        {
            Preferences.Remove(nameof(SelectedLanguage));
        }
        public void DeleteAllSortSettings()
        {
            Preferences.Remove(nameof(IsSortByName));
            Preferences.Remove(nameof(IsSortByNickName));
            Preferences.Remove(nameof(IsSortByDateAddedToDatabase));
        }
    }
}
