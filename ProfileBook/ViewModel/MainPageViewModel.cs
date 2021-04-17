using Prism.Commands;
using Prism.Navigation;
using ProfileBook.Helpers;
using ProfileBook.Model;
using ProfileBook.Service;
using ProfileBook.Service.Authorization;
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
        //fields
        private string _titlePage;
        private string _login;
        private string _password;
        private bool _isEnabled;
        private int _id;

        private IAuthenticationService _authenticationService;
        private IUserService _userService;
        private IAuthorization _authorization;
        private ObservableCollection<UserModel> _userList;
        private UserModel _userModel;
        public ICommand NavigationToSingUp { get; set; }
        public ICommand NavigateToListView { get; set; }

        public MainPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IAuthorization authorization, IUserService userService):base(navigationService)
        {
            IsEnabled = false;
            Login = string.Empty;
            Password = string.Empty;
            _authenticationService = authenticationService;
            _authorization = authorization;
            _userService = userService;
            NavigateToListView = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => IsEnabled);//For verefication and navigation
            NavigationToSingUp = new DelegateCommand(ExecuteNavigateToSignUp); //NavigationToSingUp for navigation to page SignUp(Tab Label)
            TitlePage = ($"{ nameof(MainPage)}");
        }
        public string TitlePage
        {
            get => _titlePage;
            set => SetProperty(ref _titlePage, value);
        }
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        public UserModel UserModel
        {
            get { return _userModel;}
            set { _userModel=value;}
        }
        public ObservableCollection<UserModel> UserList
        {
            get { return _userList; }
            set { _userList=value; }
        }
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        public async void ExecuteNavigateToSignUp()
        {
            await _navigationService.NavigateAsync(($"{ nameof(SignUp)}"));
        }
        public async void Execute()
        {
            if (_authenticationService.IsRelevantLoginAndPassword(UserList, Login, Password))
            {
                Id = _authenticationService.GetRegisteredUserId();
                await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainList)}"));
            }
            else
            {
                ListOfMessages.ShowInvalidloginOrPassword();
                Login = string.Empty;
                Password = string.Empty;
            }
        }
        public bool CanExecute()
        {
            return IsEnabled;
        }
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
            var userList = await _userService.GetAllUserModel();
            UserList = new ObservableCollection<UserModel>(userList);
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(Id))
            {
                if (_authorization.GetIdCurrentUser() != 0)
                {
                    _authorization.ClearAllSettings();
                }
                _authorization.SetIdCurrentUser(Id);
            }
        }
        #endregion
    }
}
