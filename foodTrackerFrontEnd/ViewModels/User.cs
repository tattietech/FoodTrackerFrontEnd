using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace foodTrackerFrontEnd.Models
{
    public class User
    {
        public string HouseholdId { get; set; }

        [JsonPropertyName("given_name")]
        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonPropertyName("family_name")]
        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public bool IsHouseholdAdmin { get; set; }
    }
}
