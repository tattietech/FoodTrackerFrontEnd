namespace foodTrackerFrontEnd.Models
{
    public class Household
    {
        public string HouseholdId { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
