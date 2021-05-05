using Acr.UserDialogs;
using ProfileBook.Resource;

namespace ProfileBook.Helpers
{
    public static class ListOfMessages
    {
        public static void ShowInvalidloginOrPassword()
        {
            UserDialogs.Instance.Alert(AppResource.Invalidloginorpassword,AppResource.Invaliddataentered);
        }
        public static void ShowPasswordOrUsernameDoesNotExist()
        {
            UserDialogs.Instance.Alert(AppResource.Passwordorusernamedoesnotexist, AppResource.Invaliddataentered);
        }
        public static void ShowRequirementsToPassword()
        {
            UserDialogs.Instance.Alert(AppResource.RequirementsToPassword, AppResource.Invaliddataentered);
        }
        public static void ShowRequirementsToLogin()
        {
            UserDialogs.Instance.Alert(AppResource.RequirementsToLogin, AppResource.Invaliddataentered);
        }
        public static void ShowRequirementsForPasswordAndConfirmPassword()
        {
            UserDialogs.Instance.Alert(AppResource.RequirementsForPasswordAndConfirmPassword, AppResource.Invaliddataentered);
        }
        public static void ShowInformationIsMissingInTheFieldsNameAndNickName()
        {
            UserDialogs.Instance.Alert(AppResource.Informationismissinginthefieldsnameandnickname, AppResource.Invaliddataentered);
        }
        public static void ShowThisLoginIsAlreadyTaken()
        {
            UserDialogs.Instance.Alert(AppResource.Thisloginisalreadytaken, AppResource.Invaliddataentered);
        }
    }
}
