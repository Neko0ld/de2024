using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication1.Windows;

public partial class OrganizerWindow : Window
{
    public OrganizerWindow()
    {
        InitializeComponent();
    }

    
    // кнопка перехода к списку заказов
    private void ListOrderButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var window = new OrderListWindow();
        window.Show();
    }
}