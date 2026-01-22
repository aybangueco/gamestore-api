using GameStoreAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GameStoreAPI.Common.Helpers;

public static class UserManager
{
    private static readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();
    
    public static string HashPassword(string password)
    {
        return _hasher.HashPassword(null, password);
    }
    
    public static bool IsPasswordMatch(string hashedPassword, string providedPassword)
    {
        var result = _hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        
        return result is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
    }
}