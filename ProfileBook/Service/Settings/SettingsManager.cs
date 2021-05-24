using ProfileBook.Helpers;
using Xamarin.Essentials;

namespace ProfileBook.Service.Settings
{
    class SettingsManager : ISettingsManager
    {
        public int AuthorizedUserID 
        {
            get => Preferences.Get(nameof(AuthorizedUserID), 0);
            set => Preferences.Set(nameof(AuthorizedUserID), value);
        }

        public int SortingType
        {
            get => Preferences.Get(nameof(SortingType), 0);
            set => Preferences.Set(nameof(SortingType), value);
        }

        public string SelectedLanguage
        {
            get =>Preferences.Get(nameof(SelectedLanguage), ListOfNames.english);
            set => Preferences.Set(nameof(SelectedLanguage), value);
        }

        public int ThemType
        {
            get => Preferences.Get(nameof(ThemType), 0);
            set => Preferences.Set(nameof(ThemType), value);
        }

        public void ClearData()
        {
            Preferences.Clear();
        }
    }
}
