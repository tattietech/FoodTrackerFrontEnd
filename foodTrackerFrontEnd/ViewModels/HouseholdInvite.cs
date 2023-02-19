using foodTrackerFrontEnd.Models;
using System.ComponentModel.DataAnnotations;

namespace foodTrackerFrontEnd.ViewModels
{
    public class HouseholdInvite
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        public bool Accepted { get; set; }

        public User From { get; set; }
    }
}
