using Prism.Commands;
using Prism.Navigation;
using ProfileBook.Helpers;
using ProfileBook.Model;
using ProfileBook.Service;
using ProfileBook.Service.User;
using ProfileBook.View;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProfileBook.ViewModel
{
    class SingUpViewModel: BaseViewModel
    {
        #region---PrivateFields---
        private string _titlePage;
        private string _login;
        private string _password;
        private string _confirmPasword;
        private bool _isEnabled;
        private IAuthenticationService _authenticationService;
        private IUserService _userService;
        private ObservableCollection<UserModel> _userList;
        private UserModel _userModel;
        #endregion
        public SingUpViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IUserService userService): base(navigationService)
        {
            IsEnabled = false;
            Login = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            _authenticationService = authenticationService;
            _userService = userService;
            SignUpCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => IsEnabled);
            CommandGoBack = new DelegateCommand(ExecuteGoBack);
            TitlePage = ($"{ nameof(SignUp)}");  
        }
        #region---PublicProperties---
        public ICommand SignUpCommand { get; set; }
        public ICommand CommandGoBack { get; set; }
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
        public string ConfirmPassword
        {
            get => _confirmPasword;
            set => SetProperty(ref _confirmPasword, value);
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        public ObservableCollection<UserModel> UserList
        {
            set { _userList = value; }
            get { return _userList; }
        }
        public UserModel UserModel
        {
            set { _userModel = value; }
            get { return _userModel; }
        }
        #endregion
        #region---Methods---
        public async void ExecuteGoBack()
        {
            var parametr = new NavigationParameters();
            parametr.Add("NewUser", UserModel);
            await _navigationService.NavigateAsync("/MainPage", parametr);
        }
        public void CreateUserModel()
        {
            UserModel = new UserModel()
            {
                Login = Login,
                Password = Password
            };
        }

        //Authentication methods
        private bool IsValidLogin()
        {
            var validationResult = true;
            if (!Validation.IsValidatedLogin(Login))
            {
                validationResult = false;
                ListOfMessages.ShowRequirementsToLogin();
            }  
            return validationResult;
        }
        private bool IsLinesMatch()
        {
            var comparisonResult = true; ;
            if(!Validation.CompareStrings(Password, ConfirmPassword))
            {
                comparisonResult = false;
                ListOfMessages.ShowRequirementsForPasswordAndConfirmPassword();
            }
            return comparisonResult;
        }
        private bool IsValidPassword()
        {
            var validationResult = true;
            if (!Validation.IsValidatedPassword(Password))
            {
                validationResult = false;
                ListOfMessages.ShowRequirementsToLogin();
            }
            return validationResult;
        }
        private bool IsLoginUnique()
        {
            var resultAuthentication = true;
            if(!_authenticationService.IsLoginUniqe(UserList, Login))
            {
                resultAuthentication = false;
                ListOfMessages.ShowThisLoginIsAlreadyTaken(); 
            }
            return resultAuthentication;
        }
        private void ClearFields()
        {
            Login = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
        public async Task AddUserModel()
        {
            CreateUserModel();
            await _userService.InsertUserModelAsync(UserModel);
        }
        public async void Execute()
        {
            if(IsValidLogin()&& IsLinesMatch()&& IsValidPassword()&& IsLoginUnique())
            {
                await AddUserModel();
                ExecuteGoBack();
            }
            else
            {
                ClearFields();
            }
        }
        public bool CanExecute()
        {
            return IsEnabled;
        }
        #endregion
        #region---OverloadedMethods---
        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            var userList = await _userService.GetAllUserModelAsync();
            if(userList!=null)
            {
                UserList = new ObservableCollection<UserModel>(userList);
            }
        }
        #endregion
    }
}
