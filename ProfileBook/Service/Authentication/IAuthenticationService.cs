using ProfileBook.Model;
using System.Threading.Tasks;

namespace ProfileBook.Service
{
    public interface IAuthenticationService
    {
       // int Id { get; set; }
        //Task<bool> IsLoginUniqeAsync(string login);
        //Task<bool> IsRelevantLoginAndPasswordAsync(string login, string password);
        Task<UserModel> SignUpAsync(string login, string password);
        Task<bool> SignInAsync(string login, string password);
    }
}
