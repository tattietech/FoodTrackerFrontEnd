namespace foodTrackerFrontEnd.Interfaces
{
    public interface IFoodStorageService
    {
        Task<string?> GetStorageId(string name);
    }
}
