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
        private readonly IAuthorizationService _authorizationService;
        private readonly INavigationService _navigationService;
        private readonly IProfileService _profileService;
        private readonly IThemService _themService;
        private readonly ILocalizationService _localizationService;
        private bool _isCheckedName;
        private bool _isCheckedNickName;
        private bool _isCheckedDataAddedToTheDB;
        private bool _isCheckedTheme;
        private string _selectedLanguage;
        private string _currentLanguage;
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public SettingsViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IProfileService profileService, IThemService themService, ILocalizationService localizationService)
        {
            _authorizationService = authorizationService;
            _navigationService = navigationService;
            _profileService = profileService;
            _themService=themService;
            _localizationService = localizationService;
            SaveCommand = new Command(SaveSettings);
            CancelCommand = new Command(CancelSettings);
            IsCheckedName = _profileService.GetValueToSortByName();
            IsCheckedNickName = _profileService.GetValueSortByNickName();
            IsCheckedDataAddedToTheDB = _profileService.GetValueSortByDateAddedToDatabase();
            IsCheckedTheme = _themService.GetValueDarkTheme();
            CurrentLanguage = _localizationService.GetValueLanguage();
        }
        private async void CancelSettings()
        {
            await _navigationService.GoBackAsync();
        }
        private void SaveSettings()
        {
            _profileService.DeleteAllSortSettings();
            //Save Settings For Sorting
            if (IsCheckedName)
            {
                _profileService.SetValueToSortByName(IsCheckedName);
            }
            else if (IsCheckedNickName)
            {
                _profileService.SetValueToSortByNickName(IsCheckedNickName);
            }
            else if (IsCheckedDataAddedToTheDB)
            {
                _profileService.SetValueToSortByDateAddedToDatabase(IsCheckedDataAddedToTheDB);
            }
            //Save Settings For Changed Language
            if (_selectedLanguage != null)
            {
                var result = CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToList().First(x => x.NativeName.Contains(_selectedLanguage.ToString()));
                CultureInfo language = new CultureInfo(result.Name);
                Thread.CurrentThread.CurrentUICulture = language;
                AppResource.Culture = language;
                if(_selectedLanguage !=_localizationService.GetValueLanguage())
                {
                    _localizationService.SetValueLanguage(_selectedLanguage);
                }
            }
            //Save settings for DarkThem
            if(_isCheckedTheme!=_themService.GetValueDarkTheme())
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
             GoBackMainList();
        }
        public async void GoBackMainList()
        {
            await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainList)}"));
        }
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
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if(args.PropertyName == nameof(IsCheckedTheme))
            {
            }
        }
    }
}
