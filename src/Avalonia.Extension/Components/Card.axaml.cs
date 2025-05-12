using System.Collections.Generic;
using System.Windows.Input;

namespace Avalonia.Extension;

/// <summary>
/// Avalonia UI Card.
/// </summary>
public partial class Card : Component
{
    /// <summary>
    /// Название карты. Внедряемое свойство.
    /// </summary>
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<Card, string>(nameof(Title));

    /// <summary>
    /// Название карты.
    /// </summary>
    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Тэги карты. Внедряемое свойство.
    /// </summary>
    public static readonly StyledProperty<List<string>> TagsProperty =
        AvaloniaProperty.Register<Card, List<string>>(nameof(Tags));

    /// <summary>
    /// Тэги карты.
    /// </summary>
    public List<string> Tags
    {
        get => GetValue(TagsProperty);
        set => SetValue(TagsProperty, value);
    }

    /// <summary>
    /// Команда карты. Внедряемое свойство
    /// </summary>
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<Card, ICommand>(nameof(Command));

    /// <summary>
    /// Команда карты.
    /// </summary>
    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public Card()
    {
        InitializeComponent();
    }
}