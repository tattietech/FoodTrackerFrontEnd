using foodTrackerFrontEnd.Models;
using foodTrackerFrontEnd.ViewModels;

namespace foodTrackerFrontEnd.Interfaces
{
    public interface IHouseholdService
    {
        Task<Household> Get();
        Task SendInvite(HouseholdInvite invite);
        Task<IEnumerable<HouseholdInvite>> GetInvites();
    }
}
