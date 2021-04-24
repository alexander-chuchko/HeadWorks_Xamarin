using ProfileBook.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Service.User
{
    public interface IUserService
    {
        Task UpdateUserModelAsync(UserModel userModel);
        Task InsertUserModelAsync(UserModel userModel);
        Task RemoveUserModelAsync(UserModel userModel);
        Task<List<UserModel>> GetAllUserModelAsync();
    }
}
