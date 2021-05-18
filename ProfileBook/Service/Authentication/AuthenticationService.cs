using ProfileBook.Model;
using ProfileBook.Service.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Service
{
    public class AuthenticationService: IAuthenticationService
    {
        private int _id;
        private readonly IUserService _userService;
        private IEnumerable<UserModel> _userList;
        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        //Method for checking for uniqueness
        public async Task<bool> IsLoginUniqeAsync(string login)
        {
            var uniquenessCheckResult = true;
            await GetAllUserModel();
            if (_userList!=null)
            {
                foreach (var user in _userList)
                {
                    if (string.Compare(user.Login, login, true)==0)
                    {
                        uniquenessCheckResult = false;
                    }
                }
            }
            return uniquenessCheckResult;
        }
        //Method for checking if the username and password match with the database
        public async Task<bool> IsRelevantLoginAndPasswordAsync(string login, string password)
        {
            var relevanceСheckResult = false;
            await GetAllUserModel();
            if(_userList!=null)
            {
                //проверяем существуют вообще логины
                foreach (var user in _userList)
                {
                    if (user.Login == login&&user.Password== password)
                    {
                        //Set in property id user
                        Id = user.Id;
                        relevanceСheckResult=true;
                    }
                }
            }
            return relevanceСheckResult;
        }
        public async Task GetAllUserModel()
        {
            _userList = await _userService.GetAllUserModelAsync();
        }
    }
}
