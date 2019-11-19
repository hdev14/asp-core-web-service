using CryptSharp;

namespace web_service.Services.Auth
{
    public static class PasswordManager
    {   
        public static string GeneratePasswordHash(string password)
        {
            return Crypter.Blowfish.Crypt(password);
        }

        public static bool CheckHashPassword(string password, string cryptedPassword)
        {
            return Crypter.CheckPassword(password, cryptedPassword);
        }
    }
}