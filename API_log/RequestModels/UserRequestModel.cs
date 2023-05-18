using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API_Log.RequestModels
{
    public class UserRequestModel
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
