using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication1.Classes;
using AvaloniaApplication1.Windows;
using Dapper;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using MySqlConnector;
using Tmds.DBus.Protocol;

namespace AvaloniaApplication1;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void Invalidate()
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs("userList"));
    }
    public MainWindow()
    {
        // запрос на заполнение списка пользователей
        using (MySqlConnection database = new MySqlConnection(Globals.connectionString))
        {
            userList = database.Query<User>(
                "SELECT userid, login, password, userroleid " +
                "FROM user").ToList();
        }
        InitializeComponent();
    }
    
    
    // список пользователей
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

    
    // кнопка авторизации
    private User currentUser = null;
    private string res = "";
    private async void Authorization_Button_OnClick(object? sender, RoutedEventArgs e)
    {
        // if (userList.FirstOrDefault(u => u.login == LoginTextBox.Text) != null)
        // {
        //     if (userList.FirstOrDefault(u => u.password == PasswordTextBox.Text) != null)
        //     {
        //         currentUser = userList.FirstOrDefault(u => u.login == LoginTextBox.Text);
        //         res = currentUser.userroleid.ToString();
        //     }
        // }

        foreach (var user in userList)
        {
            if (user.login == LoginTextBox.Text)
            {
                if (user.password == PasswordTextBox.Text)
                {
                    currentUser = user;
                    res = currentUser.userroleid.ToString();
                    break;
                }
            }
        }
        if (res == "1")
        {
            var window = new HeadsOfTheDepartmentWindow();
            window.Show();
            Close();
        }
        else if(res == "2")
        {
            var window = new OrganizerWindow();
            window.Show();
            Close();
        }
        else if (res == "3")
        {
            var window = new TechnicianWindow();
            window.Show();
            Close();
        }
        else
        {
            var box = MessageBoxManager.GetMessageBoxStandard(
                "Ошибка", "Такого пользователя не существует",
                ButtonEnum.Ok);
            var result = await box.ShowAsync();
        }
        
    }
}