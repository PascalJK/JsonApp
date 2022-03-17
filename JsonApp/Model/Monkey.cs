using System.Runtime.CompilerServices;

namespace JsonApp.Model;

public class Monkey : INotifyPropertyChanged
{
    private string _LikedIcon = MaterialDesignIcons.HeartBroken;
    private string _SavedIcon = MaterialDesignIcons.BookmarkOutline;

    #region Props
    public string Name { get; set; }
    public string Details { get; set; }
    public string Location { get; set; }
    public string Image { get; set; }
    public long Latitude { get; set; }
    public long Longitude { get; set; }
    public int Population { get; set; }

    [JsonIgnore]
    public bool CanDisableButton { get; set; }
    [JsonIgnore]
    public string LikedIcon { get => _LikedIcon; set => SetProperty(ref _LikedIcon, value); }
    [JsonIgnore]
    public string SavedIcon { get => _SavedIcon; set => SetProperty(ref _SavedIcon, value); }
    #endregion

    #region PropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value)) return false;
        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    public void CheckIfLiked()
    {
        var monkey = StaticData.LikedMonkiesList.Find(x => x.Name == Name);
        if (monkey != null)
            LikedIcon = MaterialDesignIcons.Heart;
    }
    public void CheckIfSaved()
    {
        var monkey = StaticData.SavedMonkiesList.Find(x => x.Name == Name);
        if (monkey != null)
            SavedIcon = MaterialDesignIcons.BookmarkCheck;
    }

    public void LikeItem()
    {
        var monkey = StaticData.LikedMonkiesList.SingleOrDefault(x => x.Name == Name);
        if (monkey == null)
        {
            LikedIcon = MaterialDesignIcons.Heart;
            StaticData.LikedMonkiesList.Add(this);
        }

        else
        {
            LikedIcon = MaterialDesignIcons.HeartBroken;
            StaticData.LikedMonkiesList.Remove(monkey);
        }
        var json = JsonConvert.SerializeObject(StaticData.LikedMonkiesList);
        StaticData.SaveMonkey("likesJson", json);
    }
    public void SaveItem()
    {
        var monkey = StaticData.SavedMonkiesList.SingleOrDefault(x => x.Name == Name);
        if (monkey == null)
        {
            SavedIcon = MaterialDesignIcons.BookmarkCheck;
            StaticData.SavedMonkiesList.Add(this);
        }

        else
        {
            SavedIcon = MaterialDesignIcons.BookmarkOutline;
            StaticData.SavedMonkiesList.Remove(monkey);
        }
        var json = JsonConvert.SerializeObject(StaticData.SavedMonkiesList);
        StaticData.SaveMonkey("savedJson", json);
    }
}
