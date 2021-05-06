using Prism.Commands;
using Prism.Navigation;
using ProfileBook.Helpers;
using ProfileBook.Model;
using ProfileBook.Service;
using ProfileBook.Service.User;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProfileBook.ViewModel
{
    class SingUpViewModel: BaseViewModel
    {
        #region---PrivateFields---
        private string _login;
        private string _password;
        private string _confirmPasword;
        private bool _isEnabled;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
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
            SignUpCommand = new DelegateCommand(ExecuteNavigationToSignUp, CanExecuteNavigationToSignUp).ObservesProperty(() => IsEnabled); 
        }
        #region---PublicProperties---
        public ICommand SignUpCommand { get; set; }
        public string Login
        {
            get{ return _login;}
            set{ SetProperty(ref _login, value);}
        }
        public string Password
        {
            get{ return _password;}
            set{ SetProperty(ref _password, value);}
        }
        public string ConfirmPassword
        {
            get{ return _confirmPasword;}
            set{ SetProperty(ref _confirmPasword, value);}
        }
        public bool IsEnabled
        {
            get{ return _isEnabled;}
            set{ SetProperty(ref _isEnabled, value);}
        }
        public ObservableCollection<UserModel> UserList
        {
            get{ return _userList;}
            set{ _userList = value;}
        }
        public UserModel UserModel
        {
            set{ _userModel = value;}
            get{ return _userModel;}
        }
        #endregion
        #region---Methods---
        private async void ExecuteGoBack()
        {
            var parametr = new NavigationParameters();
            parametr.Add("NewUser", UserModel);
            await _navigationService.NavigateAsync("/MainPage", parametr);
        }
        private void CreateUserModel()
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
        private async Task AddUserModel()
        {
            CreateUserModel();
            await _userService.InsertUserModelAsync(UserModel);
        }
        private async void ExecuteNavigationToSignUp()
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
        private bool CanExecuteNavigationToSignUp()
        {
            return IsEnabled;
        }
        #endregion
        #region---Overriding---
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
