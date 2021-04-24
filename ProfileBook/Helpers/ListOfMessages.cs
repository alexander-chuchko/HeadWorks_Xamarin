using Acr.UserDialogs;

namespace ProfileBook.Helpers
{
    public static class ListOfMessages
    {
        public static void ShowInvalidloginOrPassword()
        {
            UserDialogs.Instance.Alert("Invalid login or password!", "Invalid data entered");
        }
        public static void ShowPasswordOrUsernameDoesNotExist()
        {
            UserDialogs.Instance.Alert("Password and login do not exist in the database!", "Invalid data entered");
        }
        public static void ShowRequirementsToPassword()
        {
            UserDialogs.Instance.Alert("Password must be at least 8 and no more than 16 characters and contain at least one uppercase letter, one lowercase letter and one number", "Invalid data entered");
        }
        public static void ShowRequirementsToLogin()
        {
            UserDialogs.Instance.Alert("Login must be at least 4 and no more than 16 characters and not start with numbers!", "Invalid data entered");
        }
        public static void ShowRequirementsForPasswordAndConfirmPassword()
        {
            UserDialogs.Instance.Alert("Password must be at least 8 and no more than 16 characters and contain at least one uppercase letter, one lowercase letter and one number", "Invalid data entered");
        }
        public static void ShowInformationIsMissingInTheFieldsNameAndNickName()
        {
            UserDialogs.Instance.Alert("Name and NickName fields must be filled!", "Invalid data entered");
        }
        public static void ShowThisLoginIsAlreadyTaken()
        {
            UserDialogs.Instance.Alert("This login is already taken!", "Invalid data entered");
        }
    }
}
