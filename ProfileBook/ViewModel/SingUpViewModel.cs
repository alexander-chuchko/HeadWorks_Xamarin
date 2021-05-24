using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Helpers;
using ProfileBook.Model;
using ProfileBook.Resource;
using ProfileBook.Service;
using ProfileBook.Service.User;
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
            await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(SignInView)}"), parametr);
        }
        private void ClearFields()
        {
            Login = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
        private async void ExecuteNavigationToSignUp()
        {
            var result = true;
            if(!Validation.IsValidatedLogin(Login) && result)
            {
                result = false;
                await _pageDialogService.DisplayAlertAsync(AppResource.requirements_to_login, AppResource.invalid_data_entered, "OK");
            }

            if(result && !Validation.CompareStrings(Password, ConfirmPassword))
            {
                result = false;
                await _pageDialogService.DisplayAlertAsync(AppResource.requirements_for_password_and_confirm_password, AppResource.invalid_data_entered, "OK");
            }

            if(result&&!Validation.IsValidatedPassword(Password))
            {
                result = false;
                await _pageDialogService.DisplayAlertAsync(AppResource.requirements_to_password, AppResource.invalid_data_entered, "OK");
            }

            if (result)
            {
                UserModel = await _authenticationService.SignUpAsync(Login, Password);
                if (UserModel != null)
                {
                    var resultOfAction= await _userService.SaveUserModelAsync(UserModel);
                    if(resultOfAction)
                    {
                        ExecuteGoBack();
                    }
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync(AppResource.this_login_is_already_taken, AppResource.invalid_data_entered, "OK");
                }
            }
            if(!result)
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
