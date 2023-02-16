using foodTrackerFrontEnd.Models;

namespace foodTrackerFrontEnd.Interfaces
{
    public interface IFoodStorageService : IFoodTrackerApiService<FoodStorage>
    {
        Task<string?> GetStorageIdByName(string name);
    }
}
