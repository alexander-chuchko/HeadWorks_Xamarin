using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Helpers;
using ProfileBook.Model;
using ProfileBook.Resource;
using ProfileBook.Service;
using ProfileBook.Service.User;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
        private readonly IPageDialogService _pageDialogService;
        private UserModel _userModel;
        #endregion
        public SingUpViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IUserService userService, IPageDialogService pageDialogService) : base(navigationService)
        {
            IsEnabled = false;
            Login = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            _authenticationService = authenticationService;
            _userService = userService;
            _pageDialogService = pageDialogService;
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
            parametr.Add(ListOfNames.newUser, UserModel);
            await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainPage)}"), parametr);
        }
        //Authentication methods
        private async Task<bool> IsValidLoginAsync()
        {
            var validationResult = true;
            if (!Validation.IsValidatedLogin(Login))
            {
                validationResult = false;
               await _pageDialogService.DisplayAlertAsync(AppResource.requirements_to_login, AppResource.invalid_data_entered, "OK");
            }  
            return validationResult;
        }
        private async Task<bool> IsLinesMatchAsync()
        {
            var comparisonResult = true; ;
            if(!Validation.CompareStrings(Password, ConfirmPassword))
            {
                comparisonResult = false;
               await _pageDialogService.DisplayAlertAsync(AppResource.requirements_for_password_and_confirm_password, AppResource.invalid_data_entered, "OK");
            }
            return comparisonResult;
        }
        private async Task<bool> IsValidPasswordAsync()
        {
            var validationResult = true;
            if (!Validation.IsValidatedPassword(Password))
            {
                validationResult = false;
               await _pageDialogService.DisplayAlertAsync(AppResource.requirements_to_login, AppResource.invalid_data_entered, "OK");
            }
            return validationResult;
        }
        private async Task<bool> IsLoginUniqueAsync()
        {
            var resultAuthentication = true;
            if(!await _authenticationService.IsLoginUniqeAsync(Login))
            {
                resultAuthentication = false;
                await _pageDialogService.DisplayAlertAsync(AppResource.this_login_is_already_taken, AppResource.invalid_data_entered, "OK");
            }
            return resultAuthentication;
        }
        private void ClearFields()
        {
            Login = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
        private void CreateUserModel()
        {
            UserModel = new UserModel()
            {
                Login = Login,
                Password = Password
            };
        }
        private async Task AddUserModelAsync()
        {
            CreateUserModel();
            await _userService.InsertUserModelAsync(UserModel);
        }
        private async void ExecuteNavigationToSignUp()
        {
            if(await IsValidLoginAsync()&& await IsLinesMatchAsync()&& await IsValidPasswordAsync()&& await IsLoginUniqueAsync())
            {
                await AddUserModelAsync();
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
    }
}
