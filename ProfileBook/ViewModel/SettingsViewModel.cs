using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Enum;
using ProfileBook.Resource;
using ProfileBook.Service.Authorization;
using ProfileBook.Service.Localization;
using ProfileBook.Service.Profile;
using ProfileBook.Service.Theme;
using ProfileBook.Styles;
using ProfileBook.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SettingsViewModel: BindableBase
    {
        private bool _isCheckedName;
        private bool _isCheckedNickName;
        private bool _isCheckedDataAddedToTheDB;
        private bool _isCheckedTheme;
        private string _selectedLanguage;
        private string _currentLanguage;

        private readonly IAuthorizationService _authorizationService;
        private readonly ILocalizationService _localizationService;
        private readonly INavigationService _navigationService;
        private readonly IProfileService _profileService;
        private readonly IThemService _themService;
        public SettingsViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IProfileService profileService, IThemService themService, ILocalizationService localizationService)
        {
            _authorizationService = authorizationService;
            _navigationService = navigationService;
            _profileService = profileService;
            _themService=themService;
            _localizationService = localizationService;
            SaveCommand = new Command(SaveAllSettings);
            CancelCommand = new Command(CancelSettings);
            IsCheckedName = _profileService.GetValueToSortByName();
            IsCheckedNickName = _profileService.GetValueSortByNickName();
            IsCheckedDataAddedToTheDB = _profileService.GetValueSortByDateAddedToDatabase();
            IsCheckedTheme = _themService.GetValueDarkTheme();
            CurrentLanguage = _localizationService.GetValueLanguage();
        }
        #region---PublicProperties---
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public string SelectedLanguage
        {
            set { SetProperty(ref _selectedLanguage, value);}
            get { return _selectedLanguage;}
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
            _profileService.DeleteAllSortSettings();
            //Save Settings For Sorting
            SaveSortSettings();
            //Save Settings For Changed Language
            SaveLanguageSettings();
            //Save settings for DarkThem
            SaveThemeSettings();
            GoBackMainList();
        }
        private void SaveSortSettings()
        {
            if (IsCheckedName&&_profileService.GetValueToSortByName()!= IsCheckedName)
            {
                _profileService.DeleteAllSortSettings();
                _profileService.SetValueToSortByName(IsCheckedName);
            }
            else if (IsCheckedNickName&&_profileService.GetValueSortByNickName()!= IsCheckedNickName)
            {
                _profileService.DeleteAllSortSettings();
                _profileService.SetValueToSortByNickName(IsCheckedNickName);
            }
            else if (IsCheckedDataAddedToTheDB&&_profileService.GetValueSortByDateAddedToDatabase()!= IsCheckedDataAddedToTheDB)
            {
                _profileService.DeleteAllSortSettings();
                _profileService.SetValueToSortByDateAddedToDatabase(IsCheckedDataAddedToTheDB);
            }
        }
        private void SaveThemeSettings()
        {
            if (_isCheckedTheme != _themService.GetValueDarkTheme())
            {
                if (_isCheckedTheme)
                {
                    PerformThemeChange(EnumSet.Theme.Dark);
                    _themService.SetValueValueDarkTheme(IsCheckedTheme);
                }
                else
                {
                    PerformThemeChange(EnumSet.Theme.Light);
                    _themService.RemoveThemeDark();
                }
            }
        }
        private void SaveLanguageSettings()
        {
            if (_selectedLanguage != null && _selectedLanguage != _localizationService.GetValueLanguage())
            {
                var result = CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToList().First(x => x.NativeName.Contains(_selectedLanguage.ToString()));
                CultureInfo language = new CultureInfo(result.Name);
                Thread.CurrentThread.CurrentUICulture = language;
                AppResource.Culture = language;
                _localizationService.SetValueLanguage(_selectedLanguage);
            }
        }
        private async void GoBackMainList()
        {
            await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainList)}"));
        }
        private void PerformThemeChange(EnumSet.Theme theme)
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
        #endregion

        #region---Overriding---
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if(args.PropertyName == nameof(IsCheckedTheme))
            {
            }
        }
        #endregion
    }
}
