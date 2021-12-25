using Microsoft.AspNetCore.Http;

namespace WebApi.Helpers {
    public static class TokenUtils {
        public static string GetAccessToken(HttpRequest request) {
            string authorizationHeader = request.Headers["Authorization"];
            string access_token = authorizationHeader.Split(" ")[1];

            return access_token;
        }
    }
}