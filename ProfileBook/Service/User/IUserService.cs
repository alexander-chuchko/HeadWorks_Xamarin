using ProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Service.User
{
    public interface IUserService
    {
        void UpdateUserModel(UserModel userModel);
        void InsertUserModel(UserModel userModel);
        void RemoveUserModel(UserModel userModel);
        Task<List<UserModel>> GetAllUserModel();
    }
}
