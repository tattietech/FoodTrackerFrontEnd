using foodTrackerFrontEnd.Models;

namespace foodTrackerFrontEnd.Interfaces
{
    public interface IHouseholdService
    {
        Task<Household> Get();
    }
}
