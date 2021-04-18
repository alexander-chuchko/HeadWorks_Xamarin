using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Service.Authorization;
using ProfileBook.Styles;
using ProfileBook.View;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SettingsViewModel: BindableBase
    {
        private IAuthorization _authorization;
        private INavigationService _navigationService;
        private bool _isCheckedName;
        private bool _isCheckedNickName;
        private bool _isCheckedDataAddedToTheDB;
        private bool _isCheckedTheme;
        private string _titlePage;
        private bool _isVisibleButton;
        public ICommand ThemeCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public SettingsViewModel(INavigationService navigationService, IAuthorization authorization)
        {
            TitlePage = ($"{ nameof(Settings)}");
            _authorization = authorization;
            _navigationService = navigationService;
            ThemeCommand = new Command(ExecuteChangedSettingsColor);
            SaveCommand = new Command(SaveSettings);
            CancelCommand = new Command(CancelSettings);
            IsCheckedName = _authorization.IsSortByName();
            IsCheckedNickName = _authorization.IsSortByNickName();
            IsCheckedDataAddedToTheDB = _authorization.IsSortByDateAddedToDatabase();
        }
        private async void CancelSettings()
        {
            _authorization.DeleteAllSettings();
            IsCheckedName = false;
            IsCheckedNickName = false;
            IsCheckedDataAddedToTheDB = false;
            await _navigationService.GoBackAsync();
        }
        private async void SaveSettings()
        {
            _authorization.DeleteAllSettings();
            if (IsCheckedName)
            {
                _authorization.SetValueToSortByName(IsCheckedName);
            }
            else if (IsCheckedNickName)
            {
                _authorization.SetValueToSortByNickName(IsCheckedNickName);
            }
            else if (IsCheckedDataAddedToTheDB)
            {
                _authorization.SetValueToSortByDateAddedToDatabase(IsCheckedDataAddedToTheDB);
            }
            await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainList)}"));
        }
        public void ExecuteChangedSettingsColor()
        {
            OSAppTheme currentTheme = Application.Current.RequestedTheme;
            if (true)
            {
                if (false)
                {
                    //Application.Current.UserAppTheme= OSAppTheme.Light;
                }
                else
                {
                    Application.Current.Resources = new DarkTheme();
                    //Application.Current.UserAppTheme = OSAppTheme.Dark;
                }
                //SetTheme(Application.Current.RequestedTheme);

                //Application.Current.RequestedThemeChanged += (s, a) => { SetTheme(a.RequestedTheme); };
                //Application.Current.UserAppTheme = OSAppTheme.Dark//Application.Current.UserAppTheme = OSAppTheme.Dark;
            }
        }
        private void SetTheme(OSAppTheme appTheme)
        {
            switch (appTheme)
            {
                case OSAppTheme.Dark:
                    Application.Current.Resources = new DarkTheme();
                    break;
                case OSAppTheme.Light:
                    Application.Current.Resources = new LightTheme();
                    break;
                case OSAppTheme.Unspecified:
                    Application.Current.Resources = new LightTheme();
                    break;
            }
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
