using System;
using System.Collections.ObjectModel;
using System.Reactive;
using KnowledgeBase.Model;
using ReactiveUI;

namespace KnowledgeBase.Desktop.ViewModels;

public class MainPageViewModel : PageViewModelBase
{
    public ObservableCollection<Article> Articles { get; } = [];
    public ReactiveCommand<Unit, Unit> OpenCommand {get;}
    
    public MainPageViewModel()
    {
        Title = "База знаний";

        var article = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Статья 1",
            Content = "Контент статьи 1"
        };
        article.Tags.Add("тег 1");
        article.Tags.Add("тег 2");
        Articles.Add(article);
        Articles.Add(article);
        
        OpenCommand = ReactiveCommand.Create(() => {});
    }
}