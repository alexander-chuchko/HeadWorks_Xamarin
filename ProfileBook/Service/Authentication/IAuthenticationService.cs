using ProfileBook.Model;
using System.Collections.Generic;

namespace ProfileBook.Service
{
    public interface IAuthenticationService
    {
        int Id { get; set; }
        bool IsLoginUniqe(IEnumerable<UserModel> userList, string login);
        bool IsRelevantLoginAndPassword(IEnumerable<UserModel> userList, string login, string password);
    }
}
