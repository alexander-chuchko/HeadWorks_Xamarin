using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Helpers;
using ProfileBook.Model;
using ProfileBook.Resource;
using ProfileBook.Service;
using ProfileBook.Service.Authorization;
using ProfileBook.Service.User;
using ProfileBook.View;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class MainPageViewModel : BaseViewModel, INavigatedAware
    {
        #region---PrivateFields---
        private string _login;
        private string _password;
        private bool _isEnabled;
        private int _id;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IPageDialogService _pageDialogService;
        private UserModel _userModel;
        #endregion
        public MainPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IAuthorizationService authorizationService, IUserService userService, IPageDialogService pageDialogService) :base(navigationService)
        {
            IsEnabled = false;
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _userService = userService;
            _pageDialogService = pageDialogService;
            NavigateToListView = new DelegateCommand(ExecuteNavigationToMainList, CanExecuteNavigateToSignUp).ObservesProperty(() => IsEnabled);
            NavigationToSingUp = new Command(ExecuteNavigateToSignUp);
            _authorizationService.SettingDefaultSettings(); //In case of incorrect exit from the application, we check
        }
        #region---PublicProperties---
        public ICommand NavigationToSingUp { get; set; }
        public ICommand NavigateToListView { get; set; }
        
        public string Login
        {
            get{ return _login; }
            set { SetProperty(ref _login, value); }
        }
        public string Password
        {
            get{ return _password; }
            set { SetProperty(ref _password, value); }
        }
        public bool IsEnabled
        {
            get { return _isEnabled;}
            set { SetProperty(ref _isEnabled, value);}
        }
        public UserModel  UserModel
        {
            get{ return _userModel; }
            set{ SetProperty(ref _userModel, value);}
        }
        public int Id
        {
            get { return _id;}
            set { SetProperty(ref _id, value); }
        }
        #endregion
        #region---Methods---
        private async void ExecuteNavigateToSignUp()
        {
            await _navigationService.NavigateAsync(($"{ nameof(SignUp)}"));
        }
        private bool CanExecuteNavigateToSignUp()
        {
            return IsEnabled;
        }
        private async void ExecuteNavigationToMainList()
        {
            if(await _authenticationService.IsRelevantLoginAndPasswordAsync(Login, Password))
            {
                Id = _authenticationService.Id;
                await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainList)}"));
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync(AppResource.invalid_login_or_password, AppResource.invalid_data_entered, "OK");
                Login = string.Empty;
                Password = string.Empty;
            }
        }
        #endregion
        #region ---Overriding---
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(Id))
            {
                _authorizationService.SetIdCurrentUser(Id);
            }
        }
        #endregion
        #region--Iterface INavigatedAware implementation--
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue<UserModel>(ListOfNames.newUser, out UserModel userModel))
            {
                UserModel = parameters.GetValue<UserModel>(ListOfNames.newUser);
                Login = UserModel.Login;
            }
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }
        #endregion
    }
}
