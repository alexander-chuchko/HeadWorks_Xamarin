using Prism.Navigation;
using ProfileBook.Enum;
using ProfileBook.Service.Authorization;
using ProfileBook.Service.Localization;
using ProfileBook.Service.Profile;
using ProfileBook.Service.Theme;
using ProfileBook.View;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SettingsViewModel: BaseViewModel
    {
        private bool _isCheckedName;
        private bool _isCheckedNickName;
        private bool _isCheckedDataAddedToTheDB;
        private bool _isCheckedTheme;
        private string _selectedLanguage;
        private string _currentLanguage;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILocalizationService _localizationService;
        private readonly IProfileService _profileService;
        private readonly IThemService _themService;
        public SettingsViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IProfileService profileService, IThemService themService, ILocalizationService localizationService):base(navigationService)
        {
            _authorizationService = authorizationService;
            _profileService = profileService;
            _themService=themService;
            _localizationService = localizationService;
            SaveCommand = new Command(SaveAllSettings);
            CancelCommand = new Command(CancelSettings);
            DisplaySavedPageSettings();
        }
        #region---PublicProperties---
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public string SelectedLanguage
        {
            set { SetProperty(ref _selectedLanguage, value); }
            get { return _selectedLanguage; }
        }
        public string CurrentLanguage
        {
            set { SetProperty(ref _currentLanguage, value);}
            get {return _currentLanguage;}
        }
        public bool IsCheckedTheme
        {
            get {return _isCheckedTheme;}
            set {SetProperty(ref _isCheckedTheme, value);}
        }
        public bool IsCheckedName
        {
            set { SetProperty(ref _isCheckedName, value); }
            get { return _isCheckedName; }
        }
        public bool IsCheckedNickName
        {
            set { SetProperty(ref _isCheckedNickName, value); }
            get {return _isCheckedNickName;}
        }
        public bool IsCheckedDataAddedToTheDB
        {
            set { SetProperty(ref _isCheckedDataAddedToTheDB, value); }
            get  {return _isCheckedDataAddedToTheDB;}
        }
        private async void CancelSettings()
        {
            await _navigationService.GoBackAsync();
        }
        #endregion

        #region---Methods---
        private void SaveAllSettings()
        {
            //Save Settings For Sorting
            SaveSortSettings();
            //Save Settings For Changed Language
            SaveLanguageSettings();
            //Save settings for Them
            SaveThemeSettings();

            GoBackMainList();
        }
        private void DisplaySavedPageSettings()
        {
            EnumSet.SortingType sortingType = _profileService.GetValueToSort();
            switch (sortingType)
            {
                case EnumSet.SortingType.SortByName:
                    IsCheckedName = true;
                    break;
                case EnumSet.SortingType.SortByNickName:
                    IsCheckedNickName = true;
                    break;
                case EnumSet.SortingType.SortByDateAddedToDatabase:
                    IsCheckedDataAddedToTheDB = true;
                    break;
            }

            EnumSet.Theme themeType = _themService.GetValueTheme();
            switch (themeType)
            {
                case EnumSet.Theme.Light:
                    IsCheckedTheme = false;
                    break;
                case EnumSet.Theme.Dark:
                    IsCheckedTheme = true;
                    break;
            }
            CurrentLanguage = _localizationService.GetValueLanguage();
        }
        private void SaveSortSettings()
        {
            if(IsCheckedName|| IsCheckedNickName|| IsCheckedDataAddedToTheDB)
            {
                if (IsCheckedName && _profileService.GetValueToSort() != EnumSet.SortingType.SortByName)
                {
                    _profileService.SetValueToSort(EnumSet.SortingType.SortByName);
                }
                else if (IsCheckedNickName && _profileService.GetValueToSort() != EnumSet.SortingType.SortByNickName)
                {
                    _profileService.SetValueToSort(EnumSet.SortingType.SortByNickName);
                }
                else if (IsCheckedDataAddedToTheDB && _profileService.GetValueToSort() != EnumSet.SortingType.SortByDateAddedToDatabase)
                {
                    _profileService.SetValueToSort(EnumSet.SortingType.SortByDateAddedToDatabase);
                }
            }
        }
        private void SaveThemeSettings()
        {
            if(IsCheckedTheme)
            {
                if(_themService.GetValueTheme() != EnumSet.Theme.Dark)
                {
                    _themService.PerformThemeChange(EnumSet.Theme.Dark);
                    _themService.SetValueTheme(EnumSet.Theme.Dark);
                }
            }
            else
            {
                if (_themService.GetValueTheme() != EnumSet.Theme.Light)
                {
                    _themService.PerformThemeChange(EnumSet.Theme.Light);
                    _themService.SetValueTheme(EnumSet.Theme.Light);
                }
            }
        }
        private void SaveLanguageSettings()
        {
            if (SelectedLanguage != null && SelectedLanguage != _localizationService.GetValueLanguage())
            {
                _localizationService.ChangeApplicationLanguage(SelectedLanguage);
                _localizationService.SetValueLanguage(SelectedLanguage);
            }
        }
        private async void GoBackMainList()
        {
            await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainListView)}"));
        }
        #endregion
    }
}
