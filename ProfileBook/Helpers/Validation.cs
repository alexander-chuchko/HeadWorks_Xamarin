using Acr.UserDialogs;
using System.Text.RegularExpressions;

namespace ProfileBook
{
    public static class Validation
    {
        private static Regex patternForLogin;
        private static Regex patternForPassword;
        static Validation()
        {
            patternForLogin = new Regex(@"(^[^0-9]{4,16})");
            patternForPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}");
        }
        public static bool IsValidatedLogin(string login)
        {
            var validationResult = false;
            if(patternForLogin.IsMatch(login))
            {
                validationResult = true;
            }
            return validationResult;
        }
        public static bool IsValidatedPassword(string password)
        {
            var validationResult = false;
            if (patternForPassword.IsMatch(password))
            {
                validationResult = true;
            }
            return validationResult;
        }
        public static bool CompareStrings(string password, string confirmPassword)
        {
            var comparisonResult = false;
            if (string.Compare(password, confirmPassword, false)==0)
            {
                comparisonResult = true;
            }
            return comparisonResult;
        }
        //Method for checking the existence of information
        public static bool IsInformationInNameAndNickName(string name, string nickName)
        {
            var validationResult = false;
            //Дописать проверку string Empty
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(nickName))
            {
                validationResult = true;
            }
            return validationResult;
        }
    }
}
