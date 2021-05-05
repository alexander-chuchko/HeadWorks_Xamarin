using Prism.Commands;
using Prism.Navigation;
using ProfileBook.Helpers;
using ProfileBook.Model;
using ProfileBook.Service;
using ProfileBook.Service.Authorization;
using ProfileBook.Service.Localization;
using ProfileBook.Service.Profile;
using ProfileBook.Service.Theme;
using ProfileBook.Service.User;
using ProfileBook.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        #region---PrivateFields---
        private string _login;
        private string _password;
        private bool _isEnabled;
        private int _id;

        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IProfileService _profileService;
        private readonly IThemService _themService;
        private readonly ILocalizationService _localizationService;
        private ObservableCollection<UserModel> _userList;
        private UserModel _userModel;
        #endregion
        public MainPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IAuthorizationService authorizationService, IUserService userService, IProfileService profileService, IThemService themService, ILocalizationService localizationService) :base(navigationService)
        {
            IsEnabled = false;
            Login = string.Empty;
            Password = string.Empty;
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _userService = userService;
            _profileService = profileService;
            _themService = themService;
            _localizationService=localizationService;
            NavigateToListView = new DelegateCommand(ExecuteNavigationToMainList, CanExecuteNavigateToSignUp).ObservesProperty(() => IsEnabled);
            NavigationToSingUp = new Command(ExecuteNavigateToSignUp);
        }
        #region---PublicProperties---
        public ICommand NavigationToSingUp { get; set; }
        public ICommand NavigateToListView { get; set; }
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                SetProperty(ref _login, value);
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
            }
        }
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                SetProperty(ref _isEnabled, value);
            }
        }
        public UserModel UserModel
        {
            get
            {
                return _userModel;
            }
            set
            {
                SetProperty(ref _userModel, value);
            }
        }
        public ObservableCollection<UserModel> UserList
        {
            get
            {
                return _userList;
            }
            set
            {
                _userList=value;
            }
        }
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref _id, value);
            }
        }
        #endregion
        #region---Methods---
        public bool CanExecuteNavigateToSignUp()
        {
            return IsEnabled;
        }
        public async void ExecuteNavigateToSignUp()
        {
            await _navigationService.NavigateAsync(($"{ nameof(SignUp)}"));
        }
        public async void ExecuteNavigationToMainList()
        {
            if (_authenticationService.IsRelevantLoginAndPassword(UserList, Login, Password))
            {
                Id = _authenticationService.Id;
                await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainList)}"));
            }
            else
            {
                ListOfMessages.ShowInvalidloginOrPassword();
                Login = string.Empty;
                Password = string.Empty;
            }
        }
        #endregion
        #region ---Overriding---
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.Count != 0)
            {
                UserModel = parameters.GetValue<UserModel>("NewUser");
                Login = UserModel.Login;
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters) { }
        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            //In case of incorrect exit from the application, we check
            if (_authorizationService.GetIdCurrentUser() != 0 || _profileService.GetValueSortByDateAddedToDatabase() || _profileService.GetValueSortByNickName() || _profileService.GetValueToSortByName()||_themService.GetValueDarkTheme()|| _localizationService.GetValueLanguage()!= "English")
            {
                _profileService.DeleteAllSortSettings();
                _authorizationService.RemoveIdCurrentUser();
                _themService.RemoveThemeDark();
                _localizationService.RemoveLanguage();
            }
            var userList = await _userService.GetAllUserModelAsync();
            UserList = new ObservableCollection<UserModel>(userList);
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(Id))
            {
                _authorizationService.SetIdCurrentUser(Id);
            }
        }
        #endregion
    }
}
