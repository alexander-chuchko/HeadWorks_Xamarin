using ProfileBook.Model;
using System.Collections.ObjectModel;

namespace ProfileBook.Service
{
    public class AuthenticationService: IAuthenticationService
    {
        private int _id;
        //private UserModel loggedInUser;
        public AuthenticationService()
        {

        }
        /*
        public UserModel LoggedInUser
        {
            set { loggedInUser = value; }
            get { return loggedInUser; }
        }
        */
        //Method det id
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        public int GetRegisteredUserId()
        {
            return Id;
        }
        //Method for checking for uniqueness
        public bool IsLoginUniqe(ObservableCollection<UserModel> userList, string login)
        {
            var uniquenessCheckResult = true;
            if (userList.Count != 0)
            {
                //проверяем существуют вообще логины
                foreach (var user in userList)
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
        public bool IsRelevantLoginAndPassword(ObservableCollection<UserModel> userList, string login, string password)
        {
            var relevanceСheckResult = false;
            if (userList.Count != 0)
            {
                //проверяем существуют вообще логины
                foreach (var user in userList)
                {
                    if (user.Login == login&&user.Password== password)
                    {
                        //Set in property id user
                         Id = user.Id;
                        //LoggedInUser = user;
                        relevanceСheckResult=true;
                    }
                }
            }
            return relevanceСheckResult;
        }
    }
}
