using Acr.UserDialogs;
using ProfileBook.Enum;
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
        public async Task<IEnumerable<ProfileModel>> GetAllProfileModelAsync()
        {
            IEnumerable<ProfileModel> userModels = null;
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
        /*--Methods for setting the Sort flag--*/
        public EnumSet.SortingType GetValueToSort()
        {
            return (EnumSet.SortingType)_settingsManager.SortingType;
        }
        public void SetValueToSort(EnumSet.SortingType sortingType)
        {
            _settingsManager.SortingType = (int)sortingType;
        }
        public void SetDefaultValueToSort()
        {
            _settingsManager.SortingType = (int)EnumSet.SortingType.SortDefault;
        }
    }
}
