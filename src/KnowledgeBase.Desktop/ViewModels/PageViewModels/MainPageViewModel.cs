using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive;
using System.Threading.Tasks;
using KnowledgeBase.Model;
using ReactiveUI;

namespace KnowledgeBase.Desktop.ViewModels;

public class MainPageViewModel : PageViewModelBase
{
    private static HttpClient _httpClient = new();
    
    public ObservableCollection<Article> Articles { get; } = [];
    public ReactiveCommand<Unit, Unit> OpenCommand { get; }

    public MainPageViewModel()
    {
        Title = "База знаний";

        var articles = _httpClient.GetFromJsonAsAsyncEnumerable<Article>("http://localhost:5196/api/v1/articles");
        foreach (var article in articles.ToBlockingEnumerable())
        {
            Articles.Add(article);
        }

        OpenCommand = ReactiveCommand.Create(() => { });
    }
}