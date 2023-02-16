using System.ComponentModel.DataAnnotations;

namespace foodTrackerFrontEnd.Models
{
    public class FoodItem
    {
        public int Household { get; set; }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Expiry { get; set; }

        public string? BestBefore { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        public string StorageId { get; set; }

        public string GetExpiry()
        {
            DateTime expiry;
            string prefix;

            if (!string.IsNullOrEmpty(this.Expiry))
            {
                expiry = DateTime.Parse(this.Expiry);
                prefix = "Use by: ";
            }
            else if(!string.IsNullOrEmpty(this.BestBefore))
            {
                expiry = DateTime.Parse(this.BestBefore);
                prefix = "Best before";
            }
            else
            {
                return string.Empty;
            }

            return $"{prefix}{expiry.Date.ToString("dd/MM/yyyy")}";
        }
    }
}
