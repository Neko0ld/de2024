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
    private string? res="";
    //public List<User> resUserList;
    private async void Authorization_Button_OnClick(object? sender, RoutedEventArgs e)
    {
        // throw new System.NotImplementedException();
        
        foreach (var item in userList)
        {
            res = item.userroleid.ToString().Where(i => item.login == LoginTextBox.Text).ToString();
             
            
            
             // resUserList.Add(item.ToString()
             //     .Where(LoginTextBox => this.LoginTextBox.Text == item.login));
        }
        if (res == "1")
        {
            var window = new HeadsOfTheDepartment();
            window.Show();
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