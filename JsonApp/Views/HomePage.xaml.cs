namespace JsonApp.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel vm = new();

    public HomePage()
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override bool OnBackButtonPressed()
    {
        if (StaticData.ShowSavedData)
        {
            vm.MonkiesCollection.ReplaceRange(StaticData.CloudMonkiesList);
            StaticData.ShowSavedData = false;
            return true;
        }
        return false;
    }

    private void ChangeAppTheme(object sender, EventArgs e)
    {
        var currentTheme = Application.Current.UserAppTheme;
        if (currentTheme.Equals(OSAppTheme.Dark)) StaticData.SaveTheme(true);
        else StaticData.SaveTheme(false);
        StaticData.LoadTheme();
    }
}
