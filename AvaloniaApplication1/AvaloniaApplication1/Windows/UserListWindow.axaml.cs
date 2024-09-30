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

public partial class UserListWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void Invalidate()
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs("userList"));
    }
    public UserListWindow()
    {
        // запрос на заполнение списка сотрудников
        using (MySqlConnection database = new MySqlConnection(Globals.connectionString))
        {
            userList = database.Query<User>(
                "SELECT u.userid, u.login, u.password, u.status, u.lastname, u.firstname, u.middlename, " +
                "u.userroleid, ur.namerole AS userroleidString " +
                "FROM `user` u, `userrole` ur " +
                "WHERE u.userroleid = ur.userroleid").ToList();
        }
        InitializeComponent();
    }
    
    
    // список сотрудников
    private List<User> _userList = null;
    public List<User> userList
    {
        get
        {
            var res = _userList;
            
            return res;
        }
        set
        {
            _userList = value;
            Invalidate();
        }
    }

    // кнопка изменить в бд
    private void UpdateUserListButton_OnClick(object? sender, RoutedEventArgs e)
    {
        foreach (User user in userList)
        {
            using (MySqlConnection database = new MySqlConnection(Globals.connectionString))
            {
                database.Execute("UPDATE `user` SET status = @status " +
                                 "WHERE userid = @userid", user);
            }
        }
        
    }
}