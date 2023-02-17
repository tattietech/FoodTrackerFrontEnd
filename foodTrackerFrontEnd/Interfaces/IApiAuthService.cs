namespace foodTrackerFrontEnd.Interfaces
{
    public interface IApiAuthService
    {
        Task<string> GetToken();
    }
}
