using ProfileBook.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProfileBook.Service
{
    public class AuthenticationService: IAuthenticationService
    {
        private int _id;
        public AuthenticationService(){}
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        //Method for checking for uniqueness
        public bool IsLoginUniqe(IEnumerable<UserModel> userList, string login)
        {
            var uniquenessCheckResult = true;
            if (userList!=null)
            {
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
        public bool IsRelevantLoginAndPassword(IEnumerable<UserModel> userList, string login, string password)
        {
            var relevanceСheckResult = false;
            if(userList!=null)
            {
                //проверяем существуют вообще логины
                foreach (var user in userList)
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
    }
}
