using BCrypt.Net;

namespace eticaret.Areas.Admin.Models
{
    public static class PasswordHelper
    {
        // Şifreyi hashlemek için kullanılan metot
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Şifreyi doğrulamak için kullanılan metot
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
