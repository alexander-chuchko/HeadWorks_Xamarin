using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Service.Authorization;
using ProfileBook.Service.Profile;
using ProfileBook.Styles;
using ProfileBook.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SettingsViewModel: BindableBase
    {
        private IAuthorizationService _authorizationService;
        private INavigationService _navigationService;
        private IProfileService _profileService;
        private bool _isCheckedName;
        private bool _isCheckedNickName;
        private bool _isCheckedDataAddedToTheDB;
        private bool _isCheckedTheme;
        private string _titlePage;
        private bool _isVisibleButton;
        public ICommand ThemeCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public SettingsViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IProfileService profileService)
        {
            TitlePage = ($"{ nameof(Settings)}");
            _authorizationService = authorizationService;
            _navigationService = navigationService;
            _profileService = profileService;
            ThemeCommand = new Command(ExecuteChangedSettingsColor);
            SaveCommand = new Command(SaveSettings);
            CancelCommand = new Command(CancelSettings);
            IsCheckedName = _profileService.GetValueToSortByName();
            IsCheckedNickName = _profileService.GetValueSortByNickName();
            IsCheckedDataAddedToTheDB = _profileService.GetValueSortByDateAddedToDatabase();
        }
        private async void CancelSettings()
        {
            _profileService.DeleteAllSortSettings();
            IsCheckedName = false;
            IsCheckedNickName = false;
            IsCheckedDataAddedToTheDB = false;
            await _navigationService.GoBackAsync();
        }
        private async void SaveSettings()
        {
            _profileService.DeleteAllSortSettings();
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
            await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainList)}"));
        }
        public enum Theme
        {
            Light,
            Dark
        }
        public void SetTheme(Theme theme)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                switch (theme)
                {
                    case Theme.Dark:
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case Theme.Light:
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }
        }
        public void ExecuteChangedSettingsColor()
        {
            SetTheme(Theme.Dark);
        }
        public bool IsVisibleButton
        {
            set => SetProperty(ref _isVisibleButton, value);
            get => _isVisibleButton;
        }
        public string TitlePage
        {
            set => SetProperty(ref _titlePage, value);
            get => _titlePage;
        }
        public bool IsCheckedTheme
        {
            set => SetProperty(ref _isCheckedTheme, value);
            get => _isCheckedTheme;
        }
        public bool IsCheckedName
        {
            set => SetProperty(ref _isCheckedName, value);
            get => _isCheckedName;
        }
        public bool IsCheckedNickName
        {
            set => SetProperty(ref _isCheckedNickName, value);
            get => _isCheckedNickName;
        }
        public bool IsCheckedDataAddedToTheDB
        {
            set => SetProperty(ref _isCheckedDataAddedToTheDB, value);
            get => _isCheckedDataAddedToTheDB;
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if(args.PropertyName == nameof(IsCheckedTheme))
            {
                ExecuteChangedSettingsColor();
            }
        }
    }
}
