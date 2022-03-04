using System.Runtime.CompilerServices;

namespace JsonApp.ViewModel.Base;

public class BaseViewModel: INotifyPropertyChanged
{
    #region Feilds
    private bool _IsBusy, _IsLoading, _SearchbarIsVisible;
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    #region Props
    public bool IsBusy { get => _IsBusy; set => SetProperty(ref _IsBusy, value); }
    public bool IsLoading { get => _IsLoading; set => SetProperty(ref _IsLoading, value); }
    public bool SearchbarIsVisible { get => _SearchbarIsVisible; set => SetProperty(ref _SearchbarIsVisible, value); }
    #endregion

    #region ICommand
    public ICommand ClosePageCommand => new AsyncCommand(ClosePageAsync);
    #endregion

    protected virtual async Task ClosePageAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        await Application.Current.MainPage.Navigation.PopAsync();
        IsBusy = false;
    }

    #region RunTasks
    protected async Task RunIsBusyTaskAsync(Func<Task> awaitableTask, bool isLoading = false)
    {
        if (IsBusy) return;
        IsBusy = true;
        IsLoading = isLoading;
        try { await awaitableTask(); }
        finally 
        { 
            IsBusy = false; 
            IsLoading = false; 
        }
    }
    #endregion

    #region PropertyChanged
    public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value)) return false;
        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}
