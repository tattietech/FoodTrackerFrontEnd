namespace foodTrackerFrontEnd.Models
{
    public class Household
    {
        public string Id { get; set; }
        public string HouseholdId { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
