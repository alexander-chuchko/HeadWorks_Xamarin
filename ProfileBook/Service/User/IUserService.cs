using ProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Service.User
{
    public interface IUserService
    {
        Task UpdateUserModel(UserModel userModel);
        Task InsertUserModel(UserModel userModel);
        Task RemoveUserModel(UserModel userModel);
        Task<List<UserModel>> GetAllUserModel();
    }
}
