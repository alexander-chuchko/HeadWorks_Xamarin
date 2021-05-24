using ProfileBook.Model;
using ProfileBook.Model.Pfofile;

namespace ProfileBook.Extension
{
    public static class ProfileExtension
    {
        public static ProfileModel ToProfileModel(this ProfileViewModel profileViewModel)
        {
            ProfileModel profileModel = null;
            if (profileViewModel!=null)
            {
                profileModel = new ProfileModel
                {
                    Id= profileViewModel.Id,
                    UserId=profileViewModel.UserId,
                    Name = profileViewModel.Name,
                    NickName = profileViewModel.NickName,
                    Description = profileViewModel.Description,
                    ImageSource = profileViewModel.ImageSource,
                    MomentOfRegistration = profileViewModel.MomentOfRegistration
                };
            }
            return profileModel;
        }
        public static ProfileViewModel ToProfileViewModel(this ProfileModel profileModel)
        {
            ProfileViewModel profileViewModelClass = null;
            if(profileModel != null)
            {
                profileViewModelClass = new ProfileViewModel
                {
                    Id=profileModel.Id,
                    UserId = profileModel.UserId,
                    Name = profileModel.Name,
                    NickName = profileModel.NickName,
                    Description = profileModel.Description,
                    ImageSource = profileModel.ImageSource,
                    MomentOfRegistration = profileModel.MomentOfRegistration
                };
            }
            return profileViewModelClass;
        }

    }
}
