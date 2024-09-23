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

public partial class ShiftListWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void Invalidate()
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs("userList"));
    }
    public ShiftListWindow()
    {
        // запрос на заполнение списка смен
        using (MySqlConnection database = new MySqlConnection(Globals.connectionString))
        {
            shiftList = database.Query<Shift>(
                "SELECT * " +
                "FROM `shift`").ToList();
        }
        InitializeComponent();
    }
    
    
    // список смен
    private List<Shift> _shiftList = null;
    public List<Shift> shiftList
    {
        get
        {
            var res = _shiftList;

            return res;
        }
        set
        {
            _shiftList = value;
            Invalidate();
        }
    }
}