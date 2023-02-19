using foodTrackerFrontEnd.Models;

namespace foodTrackerFrontEnd
{
    public class AppState
    {
        public List<FoodStorage> StorageList { get; private set; } = new List<FoodStorage>();
        public List<FoodItem> FoodItemList { get; private set; } = new List<FoodItem>();

        public string AppBarHeading { get; private set; }

        public event Action OnChange;

        public void SetStorageList(List<FoodStorage> storage)
        {
            StorageList = storage.OrderBy(x => x.Name).ToList();
        }

        public void AddToStorageList(FoodStorage storage)
        {
            StorageList.Add(storage);
            StorageList = StorageList.OrderBy(x => x.Name).ToList();
            NotifyStateChanged();
        }

        public void RemoveFromStorageList(FoodStorage storage)
        {
            StorageList.Remove(storage);
            NotifyStateChanged();
        }

        public void SetFoodItemList(List<FoodItem> item)
        {
            FoodItemList = item;
        }

        public void AddToFoodItemList(FoodItem item)
        {
            FoodItemList.Add(item);
            NotifyStateChanged();
        }

        public void RemoveFromFoodItemList(FoodItem item)
        {
            FoodItemList.Remove(item);
            NotifyStateChanged();
        }

        public void SetAppBarHeading(string heading)
        {
            AppBarHeading = heading;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
