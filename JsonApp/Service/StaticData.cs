namespace JsonApp.Service;

public static class StaticData
{
    //for give me if i wrote monkies wrong (was too lazy to google)😅😅
    public static List<Monkey> CloudMonkiesList = new();
    public static List<Monkey> LikedMonkiesList = new();
    public static List<Monkey> SavedMonkiesList = new();

    public static string LikesJson { get; set; } = "";
    public static string SavedJson { get; set; } = "";
    public static bool ShowSavedData { get; set; }

    public static void GetLikedMonkys()
    {
        LikedMonkiesList.Clear();
        LikesJson = Preferences.Get("likesJson", string.Empty);
        IEnumerable<Monkey> monkeys = JsonConvert.DeserializeObject<IEnumerable<Monkey>>(LikesJson);

        if (monkeys == null) return;
        LikedMonkiesList.AddRange(monkeys);
    }
    public static void GetSavedMonkys()
    {
        SavedMonkiesList.Clear ();
        SavedJson = Preferences.Get("savedJson", string.Empty);
        IEnumerable<Monkey> monkeys = JsonConvert.DeserializeObject<IEnumerable<Monkey>>(SavedJson);

        if (monkeys == null)  return;
        SavedMonkiesList.AddRange(monkeys);
    }
    public static void LoadTheme()
    {
        var isLightTheme = Preferences.Get("appTheme", true);
        Application.Current.UserAppTheme = !isLightTheme ? OSAppTheme.Dark : OSAppTheme.Light;
    }
    public static void SaveMonkey(string key,string monkeyJson)
    {
        Preferences.Set(key, monkeyJson);
    }
    public static void SaveTheme(bool isLight = true)
    {
        Preferences.Set("appTheme", isLight);
    }
}
