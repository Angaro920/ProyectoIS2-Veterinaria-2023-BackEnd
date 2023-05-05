using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.Utilities
{
    public class TokenManagement
    {
        [JsonProperty("ValidateIssuerSigningKey")]
        public bool ValidateIssuerSigningKey { get; set; }
        [JsonProperty("IssuerSigningKey")]
        public string IssuerSigningKey { get; set; } = String.Empty;
        [JsonProperty("ValidateIssuer")]
        public bool ValidateIssuer { get; set; } = true;
        [JsonProperty("ValidIssuer")]
        public string ValidIssuer { get; set; } = String.Empty;
        [JsonProperty("ValidateAudience")]
        public bool ValidateAudience { get; set; } = true;
        [JsonProperty("ValidAudience")]
        public string ValidAudience { get; set; } = String.Empty;
        [JsonProperty("RequireExpirationTime")]
        public bool RequireExpirationTime { get; set; }
        [JsonProperty("FlagExpirationTimeHours")]
        public bool FlagExpirationTimeHours { get; set; }
        [JsonProperty("ExpirationTimeHours")]
        public int ExpirationTimeHours { get; set; }
        [JsonProperty("FlagExpirationTimeMinutes")]
        public bool FlagExpirationTimeMinutes { get; set; }
        [JsonProperty("ExpirationTimeMinutes")]
        public int ExpirationTimeMinutes { get; set; }
        [JsonProperty("ValidateLifetime")]
        public bool ValidateLifetime { get; set; } = true;
        [JsonProperty("RefreshExpiration")]
        public int RefreshExpiration { get; set; }
    }
}
