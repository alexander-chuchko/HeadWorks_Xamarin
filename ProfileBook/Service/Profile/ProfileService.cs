using Acr.UserDialogs;
using ProfileBook.Model;
using ProfileBook.Service.Settings;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Service.Profile
{
    public class ProfileService : IProfileService
    {
        private IRepository _repository;
        private ISettingsManager _settingsManager;
        public ProfileService(IRepository repository, ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }
        public async Task<List<ProfileModel>> GetAllProfileModelAsync()
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

        public async Task InsertProfileModelAsync(ProfileModel profileModel)
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
        public async Task RemoveProfileModelAsync(ProfileModel profileModel)
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
        public async Task UpdateProfileModelAsync(ProfileModel profileModel)
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
        /*--Methods for setting the SortByName flag--*/
        public bool GetValueToSortByName()
        {
            return _settingsManager.IsSortByName;
        }
        public void SetValueToSortByName(bool value)
        {
            _settingsManager.IsSortByName = value;
        }
        /*--Methods for setting the SortByNickName flag--*/
        public bool GetValueSortByNickName()
        {
            return _settingsManager.IsSortByNickName;
        }
        public void SetValueToSortByNickName(bool value)
        {
            _settingsManager.IsSortByNickName = value;
        }
        /*--Methods for setting the SortByDateAddedToDatabase flag--*/
        public bool GetValueSortByDateAddedToDatabase()
        {
            return _settingsManager.IsSortByDateAddedToDatabase;
        }
        public void SetValueToSortByDateAddedToDatabase(bool value)
        {
            _settingsManager.IsSortByDateAddedToDatabase = value;
        }
        public void DeleteAllSortSettings()
        {
            _settingsManager.DeleteAllSortSettings();
        }
    }
}
