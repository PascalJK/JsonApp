namespace JsonApp.Resources.Templates;

public class IconButtonTemplate : ContentView
{
    public static readonly BindableProperty IconGlyphProperty = BindableProperty.Create(nameof(IconGlyph), typeof(string), typeof(IconButtonTemplate), MaterialDesignIcons.FileQuestion);
    public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(IconButtonTemplate), null);
    public static readonly BindableProperty ClickedCommandParameterProperty = BindableProperty.Create(nameof(ClickedCommandParameter), typeof(object), typeof(IconButtonTemplate), null);

    public string IconGlyph
    {
        get => (string)GetValue(IconGlyphProperty);
        set => SetValue(IconGlyphProperty, value);
    }
    public ICommand ClickedCommand
    {
        get => (ICommand)GetValue(ClickedCommandProperty);
        set => SetValue(ClickedCommandProperty, value);
    }
    public object ClickedCommandParameter
    {
        get => (object)GetValue(ClickedCommandParameterProperty);
        set => SetValue(ClickedCommandParameterProperty, value);
    }
}
