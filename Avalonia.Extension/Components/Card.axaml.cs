using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Avalonia.Extension;

public partial class Card : Component
{
    public static readonly StyledProperty<string> TitleProperty = 
        AvaloniaProperty.Register<Card, string>(nameof(Title));
    public string Title
    {
        get => GetValue(TitleProperty); 
        set => SetValue(TitleProperty, value);
    }
    
    public static readonly StyledProperty<List<string>> TagsProperty =
        AvaloniaProperty.Register<Card, List<string>>(nameof(Tags));
    public List<string> Tags
    {
        get => GetValue(TagsProperty); 
        set => SetValue(TagsProperty, value);
    }
    
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<Card, ICommand>(nameof(Command));
    public ICommand Command
    {
        get => GetValue(CommandProperty); 
        set => SetValue(CommandProperty, value);
    }
    
    public Card()
    {
        InitializeComponent();
    }
}