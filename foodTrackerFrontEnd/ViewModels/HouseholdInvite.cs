using foodTrackerFrontEnd.Models;
using System.ComponentModel.DataAnnotations;

namespace foodTrackerFrontEnd.ViewModels
{
    public class HouseholdInvite
    {
        public string Id { get; set; }

        [Required, EmailAddress]
        public string Recipient { get; set; }

        public bool Accepted { get; set; }

        public User From { get; set; }
    }
}
