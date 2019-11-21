using CryptSharp;
using web_service.Models;

namespace web_service.Services.Auth
{
    public static class AuthManager
    {
        public static bool AuthenticateUser(User user, string password)
        {
            if (user != null && CheckUserPassowrd(password, user.Password))
                return true;
            
            return false;
        }

        public static string encrypt(string password)
        {
            return Crypter.Blowfish.Crypt(password);
        }

        private static bool CheckUserPassowrd(string password, string cryptedPassword)
        {
            return Crypter.CheckPassword(password, cryptedPassword);
        }
    }
}