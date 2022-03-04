namespace JsonApp.ViewModel;

public class HomePageViewModel : BaseViewModel
{
    readonly string url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";

    #region Prop
    public ObservableRangeCollection<Monkey> MonkiesCollection { get; set; } = new();
    #endregion

    #region ICommand
    public ICommand LoadSavedMonkeysCommand => new Command(LoadSavedMonkeys);
    public ICommand LikeItemCommand => new Command<Monkey>(LikeItemAsync);
    public ICommand SaveItemCommand => new Command<Monkey>(SaveItemAsync);
    #endregion

    public HomePageViewModel()
    {
        Task.Run(() =>
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await RunIsBusyTaskAsync(GetMonkiesAsync, true);
                Console.WriteLine("Remove Simplify this line of code....!");
            });
            Console.WriteLine("This is another method use lamda ex for 1 methode!");
        });
    }

    private async Task GetMonkiesAsync()
    {
        StaticData.GetSavedMonkys();
        StaticData.GetLikedMonkys();
        var httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();

        IEnumerable<Monkey> monkeys = JsonConvert.DeserializeObject<IEnumerable<Monkey>>(json);

        foreach (Monkey monkey in monkeys)
        {
            monkey.CheckIfSaved();
            monkey.CheckIfLiked();
            StaticData.CloudMonkiesList.Add(monkey);
        }

        MonkiesCollection.ReplaceRange(StaticData.CloudMonkiesList);
    }
    private void LoadSavedMonkeys()
    {
        StaticData.ShowSavedData = true;
        MonkiesCollection.ReplaceRange(StaticData.SavedMonkiesList);
    }
    private void LikeItemAsync(Monkey monkey)
    {
        monkey.LikeItem();
    }
    private void SaveItemAsync(Monkey monkey)
    {
        if (monkey == null) throw new NullReferenceException();

        monkey.SaveItem();
    }
}
