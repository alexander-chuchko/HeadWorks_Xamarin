﻿using Acr.UserDialogs;
using ProfileBook.Model;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Service.User
{
    public class UserService: IUserService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
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
        public async Task<bool> DeleteUserModelAsync(UserModel userModel)
        {
            bool resultOfAction = false;
            try
            {
                if (userModel != null)
                {
                    await _repository.DeleteAsync(userModel);
                    resultOfAction = true;
                }
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            return resultOfAction;
        }
        public async Task<bool> SaveUserModelAsync(UserModel userModel)
        {
            bool resultOfAction = false;
            try
            {
                await _repository.InsertAsync<UserModel>(userModel);
                resultOfAction = true;
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            return resultOfAction;
        }
        public async Task<bool> UpdateUserModelAsync(UserModel userModel)
        {
            bool resultOfAction = false;
            try
            {
                if(userModel!=null)
                {
                    await _repository.UpdateAsync<UserModel>(userModel);
                    resultOfAction = true;
                } 
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            return resultOfAction;
        }
    }
}
