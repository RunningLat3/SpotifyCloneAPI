using System.Text;
using Microsoft.AspNetCore.Http;

namespace SpotifyCloneAPI.API.Helpers;

public static class TokenUtils
{
    private static readonly Random _random = new Random();
    private static readonly string REFRESH_TOKEN_KEY = "refreshToken";
    public static string GetAccessToken(HttpRequest request)
    {
        string authorizationHeader = request.Headers["Authorization"];
        if (String.IsNullOrEmpty(authorizationHeader))
            throw new AppException("Missing authorization header");

        string access_token = authorizationHeader.Split(" ").Last();
        return access_token;
    }

    public static string RandomString(int size, bool lowerCase = false)
    {
        var builder = new StringBuilder(size);

        // Unicode/ASCII Letters are divided into two blocks
        // (Letters 65–90 / 97–122):
        // The first group containing the uppercase letters and
        // the second group containing the lowercase.  

        // char is a single Unicode character  
        char offset = lowerCase ? 'a' : 'A';
        const int lettersOffset = 26; // A...Z or a..z: length=26  

        for (var i = 0; i < size; i++)
        {
            var @char = (char)_random.Next(offset, offset + lettersOffset);
            builder.Append(@char);
        }

        return lowerCase ? builder.ToString().ToLower() : builder.ToString();
    }

    public static void SetTokenCookie(HttpResponse response, string token, int expiresIn)
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.UtcNow.AddSeconds(expiresIn),
        };

        response.Cookies.Append(REFRESH_TOKEN_KEY, token, cookieOptions);
    }

    public static void RemoveTokenCookie(HttpResponse response)
    {
        response.Cookies.Delete(REFRESH_TOKEN_KEY);
    }
}
