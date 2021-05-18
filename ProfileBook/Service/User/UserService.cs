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
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;

        public UserService(INavigationService navigationService, IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;
        }
        public async Task<IEnumerable<UserModel>> GetAllUserModelAsync()
        {
            IEnumerable<UserModel> userModels = null;
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
        public async Task RemoveUserModelAsync(UserModel userModel)
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
        public async Task InsertUserModelAsync(UserModel userModel)
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
        public async Task UpdateUserModelAsync(UserModel userModel)
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
