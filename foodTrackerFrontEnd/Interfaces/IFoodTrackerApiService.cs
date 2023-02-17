using foodTrackerFrontEnd.Models;
using System.Collections.ObjectModel;

namespace foodTrackerFrontEnd.Interfaces
{
    public interface IFoodTrackerApiService<T>
    {
        Task<IEnumerable<T>> List(string? storageId = null);
        Task<T> Add(T item);

        Task Delete(string id);
    }
}
