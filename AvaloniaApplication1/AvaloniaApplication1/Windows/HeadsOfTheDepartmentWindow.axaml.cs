using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Classes;
using Dapper;
using MySqlConnector;

namespace AvaloniaApplication1.Windows;

public partial class HeadsOfTheDepartmentWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void Invalidate()
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs("userList"));
    }
    public HeadsOfTheDepartmentWindow()
    {
        InitializeComponent();
    }
    
    
    // кнопка перехода к списку заказов
    private void ListOrderButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var window = new OrderListWindow();
        window.Show();
    }
    
    
    // кнопка перехода к списку сотрудников
    private void ListUserButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var window = new DepartmentUserListWindow();
        window.Show();
    }

    
    // кнопка перехода к списку смен
    private void ListShiftButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var window = new ShiftListWindow();
        window.Show();
    }
}