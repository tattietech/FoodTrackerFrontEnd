namespace foodTrackerFrontEnd.Models
{
    public class FoodItem
    {
        public int Household { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string? Expiry { get; set; }

        public string? BestBefore { get; set; }

        public int Quantity { get; set; }
    }
}
