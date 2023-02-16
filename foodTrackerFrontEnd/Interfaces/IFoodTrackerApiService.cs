using System.Collections.ObjectModel;

namespace foodTrackerFrontEnd.Interfaces
{
    public interface IFoodTrackerApiService<T>
    {
        public List<T> LocalList { get; set; }
        Task List(string? storageId = null);
        Task<T> Add(T item);
    }
}
