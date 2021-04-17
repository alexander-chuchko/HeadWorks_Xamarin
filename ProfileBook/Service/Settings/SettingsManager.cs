using System;
using Xamarin.Essentials;

namespace ProfileBook.Service.Settings
{
    class SettingsManager : ISettingsManager
    {
        public int Id 
        {
            get => Preferences.Get(nameof(Id), 0);
            set =>Preferences.Set(nameof(Id), value); 
        }
        public void RemoveCurrentId()
        {
            Preferences.Remove(nameof(Id));
        }

        public void ClearAllSettings()
        {
            Preferences.Clear();
        }
        public void DeleteAllSettings()
        {
            Preferences.Remove(nameof(IsSortByName));
            Preferences.Remove(nameof(IsSortByNickName));
            Preferences.Remove(nameof(IsSortByDateAddedToDatabase));
        }
        public bool IsSortByName
        {
            get => Preferences.Get(nameof(IsSortByName), false);
            set => Preferences.Set(nameof(IsSortByName), value);
        }
        public bool IsSortByNickName
        {
            get => Preferences.Get(nameof(IsSortByNickName), false);
            set => Preferences.Set(nameof(IsSortByNickName), value);
        }
        public bool IsSortByDateAddedToDatabase
        {
            get => Preferences.Get(nameof(IsSortByNickName), false);
            set => Preferences.Set(nameof(IsSortByNickName), value);
        }
    }
}
