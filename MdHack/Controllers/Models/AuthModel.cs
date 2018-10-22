using Newtonsoft.Json;

namespace MdHack.Controllers.Models
{
    public class AuthModel
    {
        public AuthModel(string userId, string accessToken)
        {
            UserId = userId;
            AccessToken = accessToken;
        }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string Passport { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}