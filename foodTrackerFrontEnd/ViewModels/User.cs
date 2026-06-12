using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace foodTrackerFrontEnd.Models
{
    public class User
    {
        [JsonPropertyName("custom:householdId")]
        [JsonProperty("custom:householdId")]
        public string CurrentHousehold { get; set; }

        [JsonPropertyName("given_name")]
        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonPropertyName("family_name")]
        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public List<Household> Households { get; set; } = new List<Household>();

        public List<string> HouseholdsAsAdmin { get; set; } = new List<string>();

        public bool IsHouseholdAdmin(string householdId)
        {
            return HouseholdsAsAdmin.Contains(householdId);
        }
    }
}
