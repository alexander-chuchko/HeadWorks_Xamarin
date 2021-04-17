using ProfileBook.Model;
using System.Collections.ObjectModel;

namespace ProfileBook.Service
{
    public interface IAuthenticationService
    {
        int Id { get; set; }
        int GetRegisteredUserId();
        //UserModel LoggedInUser {set; get;}
        bool IsLoginUniqe(ObservableCollection<UserModel> userList, string login);
        bool IsRelevantLoginAndPassword(ObservableCollection<UserModel> userList, string login, string password);
    }
}
