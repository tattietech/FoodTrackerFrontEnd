using System.ComponentModel.DataAnnotations;

namespace foodTrackerFrontEnd.Models
{
    public class FoodStorage
    {
        public int Household { get; set; }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
