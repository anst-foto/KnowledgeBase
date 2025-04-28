using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Media;
using ReactiveUI.Fody.Helpers;

namespace KnowledgeBase.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<PagesItem> Pages { get; } = 
    [
        new()
        {
            PageViewModel = new MainPageViewModel(),
            Icon = Application.Current?.Resources["Book_Regular"] as StreamGeometry
        }
    ];
    
    [Reactive] public PageViewModelBase ActivePageViewModel { get; set; }

    public MainWindowViewModel()
    {
        ActivePageViewModel = Pages[0].PageViewModel;
    }
}

public class PagesItem
{
    public required PageViewModelBase PageViewModel { get; init; }
    public string Title => PageViewModel.Title;
    public StreamGeometry? Icon { get; init; }
}