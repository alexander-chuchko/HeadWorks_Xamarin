using Acr.UserDialogs;
using ProfileBook.Model;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Service.Profile
{
    public class ProfileService : IProfileService
    {
        private IRepository _repository;
        public ProfileService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ProfileModel>> GetAllProfileModel()
        {
            List<ProfileModel> userModels = null;
            try
            {
                userModels = await _repository.GetAllAsync<ProfileModel>();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            return userModels;
        }

        public async void InsertProfileModel(ProfileModel profileModel)
        {
            try
            {
                await _repository.InsertAsync<ProfileModel>(profileModel);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
        }
        public async void RemoveProfileModel(ProfileModel profileModel)
        {
            try
            {
                if (profileModel != null)
                {
                    await _repository.DeleteAsync(profileModel);
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
        }
        public async void UpdateProfileModel(ProfileModel profileModel)
        {
            try
            {
                if (profileModel != null)
                {
                    await _repository.UpdateAsync<ProfileModel>(profileModel);
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
        }
    }
}
