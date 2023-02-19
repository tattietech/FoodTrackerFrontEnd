using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace foodTrackerFrontEnd.Models
{
    public class FoodItem
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Expiry { get; set; }

        public string? BestBefore { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        public string StorageId { get; set; }

        public Color DateColor()
        {
            if(this.GetExpiry() < DateTime.Today)
            {
                return Color.Error;
            }
            else if(this.GetExpiry().AddDays(-2) <= DateTime.Today)
            {
                return Color.Warning;
            }
            else
            {
                return Color.Success;
            }
        }

        public DateTime GetExpiry()
        {
            DateTime expiry = DateTime.Now;

            if (!string.IsNullOrEmpty(this.Expiry))
            {
                expiry = DateTime.Parse(this.Expiry);
            }
            else if (!string.IsNullOrEmpty(this.BestBefore))
            {
                expiry = DateTime.Parse(this.BestBefore);
            }

            return expiry;
        }

        public string GetExpiryString()
        {
            string prefix = string.Empty;

            if (!string.IsNullOrEmpty(this.Expiry))
            {
                prefix = "Use by: ";
            }
            else if(!string.IsNullOrEmpty(this.BestBefore))
            {
                prefix = "Best before";
            }

            return $"{prefix}{this.GetExpiry().ToString("dd/MM/yyyy")}";
        }
    }
}
