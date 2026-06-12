using foodTrackerFrontEnd.Models;
using foodTrackerFrontEnd.ViewModels;

namespace foodTrackerFrontEnd.Interfaces
{
    public interface IHouseholdService
    {
        Task<Household> Get();
        Task SendInvite(HouseholdInvite invite);
        Task<IEnumerable<HouseholdInvite>> GetInvites();

        Task AcceptInvite(string inviteId);

        Task DeclineInvite(string inviteId);

        Task Switch(string id);
    }
}
