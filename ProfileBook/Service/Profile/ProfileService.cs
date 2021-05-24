using Acr.UserDialogs;
using ProfileBook.Enum;
using ProfileBook.Service.Settings;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProfileBook.Model.Pfofile;

namespace ProfileBook.Service.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingsManager;
        public ProfileService(IRepository repository, ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }
        public async Task<IEnumerable<ProfileModel>> GetProfileListAsync()
        {
            IEnumerable<ProfileModel> userModelsById = null;
            try
            {
                var resultOfGettingAllProfiles = await _repository.GetAllAsync<ProfileModel>();
                userModelsById = resultOfGettingAllProfiles .Where(x => x.UserId == _settingsManager.AuthorizedUserID).ToList();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            return userModelsById;
        }
        public async Task<bool> SaveOrUpdateProfileModelToStorageAsync(ProfileModel profileModel)
        {
            bool resultOfAction = false;
            try
            {
                if(profileModel!=null)
                {
                    if (profileModel.UserId == 0)
                    {
                        profileModel.UserId = _settingsManager.AuthorizedUserID;
                        await _repository.InsertAsync<ProfileModel>(profileModel);
                        resultOfAction = true;
                    }
                    else
                    {
                        await _repository.UpdateAsync<ProfileModel>(profileModel);
                        resultOfAction = true;
                    }
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            return resultOfAction;
        }
        public async Task<bool> DeleteProfileModelToStorageAsync(ProfileModel profileModel)
        {
            bool resultOfAction = false;
            try
            {
                if (profileModel != null)
                {
                    await _repository.DeleteAsync(profileModel);
                    resultOfAction = true;
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            return resultOfAction;
        }
        public async Task<bool> UpdateProfileModelToStorageAsync(ProfileModel profileModel)
        {
            bool resultOfAction = false;
            try
            {
                if (profileModel != null)
                {
                    resultOfAction = true;
                    await _repository.UpdateAsync<ProfileModel>(profileModel);
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            return resultOfAction;
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
    }
}
