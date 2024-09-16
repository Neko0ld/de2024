using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
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
        // запрос на заполнение списка заказов
        using (MySqlConnection database = new MySqlConnection(Globals.connectionString))
        {
            orderList = database.Query<Order>(
                "SELECT * " +
                "FROM `order`").ToList();
        }
        InitializeComponent();
    }
    
    // список заказов
    private List<Order> _orderList = null;
    public List<Order> orderList
    {
        get
        {
            var res = _orderList;

            return res;
        }
        set
        {
            _orderList = value;
            Invalidate();
        }
    }
}