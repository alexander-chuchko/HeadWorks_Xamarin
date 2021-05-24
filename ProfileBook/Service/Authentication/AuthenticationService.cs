using ProfileBook.Model;
using ProfileBook.Service.Settings;
using ProfileBook.Service.User;
using System.Threading.Tasks;

namespace ProfileBook.Service
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ISettingsManager _settingsManager;
        public AuthenticationService(IUserService userService, ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            _userService = userService;
        }
        public async Task<UserModel> SignUpAsync(string login, string password)
        {
            var uniquenessCheckResult = true;
            UserModel userModel = null;
            var userList = await _userService.GetAllUserModelAsync();
            if (userList != null)
            {
                foreach (var user in userList)
                {
                    if (string.Compare(user.Login, login, true) == 0)
                    {
                        uniquenessCheckResult = false;
                    }
                }
            }
            if(uniquenessCheckResult)
            {
                userModel = new UserModel()
                {
                    Login = login,
                    Password = password
                };
            }
            return userModel;
        }
        public async Task<bool> SignInAsync(string login, string password)
        {
            var relevanceСheckResult = false;
            var listOfUserModels = await _userService.GetAllUserModelAsync();
            if (listOfUserModels != null)
            {
                foreach (var userModel in listOfUserModels)
                {
                    if (userModel.Login == login && userModel.Password == password)
                    {
                        _settingsManager.AuthorizedUserID = userModel.Id;
                        relevanceСheckResult = true;
                    }
                }
            }
            return relevanceСheckResult;
        }
    }
}
