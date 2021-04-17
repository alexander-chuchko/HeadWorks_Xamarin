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
        //fields of class
        private string _titlePage;
        private string _login;
        private string _password;
        private string _confirmPasword;
        private bool _isEnabled;

        public ICommand SignUpCommand { get; set; }
        public ICommand CommandGoBack { get; set; }
        //private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        private IUserService _userService;
        private ObservableCollection<UserModel> _userList;
        private UserModel _userModel;
        public SingUpViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IUserService userService): base(navigationService)
        {
            IsEnabled = false;
            Login = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            //_navigationService = navigationService;
            _authenticationService = authenticationService;
            _userService = userService;
            SignUpCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => IsEnabled);
            CommandGoBack = new DelegateCommand(ExecuteGoBack);
            TitlePage = ($"{ nameof(SignUp)}");  
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
        public void AddUserModel()
        {
            CreateUserModel();
            _userService.InsertUserModel(UserModel);
        }
        public void Execute()
        {
            var executionResult = false;
            if (Validation.IsValidatedLogin(Login))
            {
                if(Validation.CompareStrings(Password, ConfirmPassword))
                {
                    if(Validation.IsValidatedPassword(Password))
                    {
                        if(_authenticationService.IsLoginUniqe(UserList, Login))
                        {
                            executionResult = true;
                            AddUserModel();
                            ExecuteGoBack(); 
                        }
                        else
                        {
                            ListOfMessages.ShowThisLoginIsAlreadyTaken();
                        }
                    }
                    else
                    {
                        ListOfMessages.ShowRequirementsToPassword();
                    }
                }
                else
                {
                    ListOfMessages.ShowRequirementsForPasswordAndConfirmPassword();
                }
            }
            else
            {
                ListOfMessages.ShowRequirementsToLogin();
            }
            if(!executionResult)
            {
                Login = string.Empty;
                Password = string.Empty;
                ConfirmPassword = string.Empty;
            }
        }
        public bool CanExecute()
        {
            return IsEnabled;
        }
        //Метод срабатывает при переходе на страницу View
        #region---Overrides---
        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            //Написать проверку
            //Get a profileList from the repository
            //var userList = await _repository.GetAllAsync<UserModel>();
            var userList = await _userService.GetAllUserModel();
            //Set a profilelist in the ProfileList
            UserList = new ObservableCollection<UserModel>(userList);
        }
        #endregion
    }
}
