using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Model;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Service.User
{
    public class UserService: IUserService
    {
        private INavigationService _navigationService;
        private IRepository _repository;
        public UserService(INavigationService navigationService, IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;
        }
        public async Task<List<UserModel>> GetAllUserModel()
        {
            List<UserModel> userModels = null;
            try
            {
                userModels = await _repository.GetAllAsync<UserModel>();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            return userModels;
        }
        public async Task RemoveUserModel(UserModel userModel)
        {
            try
            {
                if (userModel != null)
                {
                    await _repository.DeleteAsync(userModel);
                }
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
        }
        public async Task InsertUserModel(UserModel userModel)
        {
            try
            {
                await _repository.InsertAsync<UserModel>(userModel);
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
        }
        public async Task UpdateUserModel(UserModel userModel)
        {
            try
            {
                if(userModel!=null)
                {
                    await _repository.UpdateAsync<UserModel>(userModel);
                } 
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
        }
    }
}
