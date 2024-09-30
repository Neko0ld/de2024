Шпора для себя

### Содержание:

- [1. Установка Avalonia](#1-установка-avalonia)
- [2. Установка Dapper](#2-dapper)
- [3. Интерфейс INotifyPropertyChanged](#3-интерфейс-inotifypropertychanged)
- [4. MessageBox для Avalonia](#4-messagebox-для-avalonia)
- [5. База List](#5-база-list)
- [6. Data Grid](#6-data-grid)
- [7. Открытие нового окна](#7-открытие-нового-окна)
- [8. Авторизация](#8-авторизация)

### <b>1. Установка Avalonia</b>

Прописать в командную строку (win + r => cmd)
- dotnet new install Avalonia.Templates

[↑ Содержание ↑](#содержание)

### <b>2. Dapper</b>

Команды импорта:
- dotnet add package MySqlConnector
- dotnet add package Dapper

```c#
using (MySqlConnection database = new MySqlConnection(Globals.connectionString))
        {
            userList = database.Query<User>(
                "SELECT userid, login, password, userroleid " +
                "FROM user").ToList();
        }
```

Глобальный класс в папке <b>Classes</b>

```c#
public class Globals
{
    public static string connectionString = "Server=kolei.ru; User ID=свой; Password=свой; Database=свой";
}
```

[↑ Содержание ↑](#содержание)

### <b>3. Интерфейс INotifyPropertyChanged</b>

```c#
public partial class MainWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void Invalidate()
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs("свой список"));
    }
    ...
}
```

[↑ Содержание ↑](#содержание)

### <b>4. MessageBox для Avalonia</b>

Команда импорта:
- dotnet add package MessageBox.Avalonia

```c#
  var box = MessageBoxManager
            .GetMessageBoxStandard("Caption", "Are you sure you would like to delete appender_replace_page_1?",
                ButtonEnum.YesNo);

        var result = await box.ShowAsync();
```

[Инструкция как пользоваться](https://github.com/AvaloniaCommunity/MessageBox.Avalonia)

[↑ Содержание ↑](#содержание)

### <b>5. База List</b>

```c#
    private List<User> _userList = null;
    public List<User> userList
    {
        get
        {
            var res = _userList;
            ...
            return res;
        }
        set
        {
            _userList = value;
            Invalidate();
        }
    }
```

[↑ Содержание ↑](#содержание)

### <b>6. Data Grid</b>

Команда импорта:
- dotnet add package Avalonia.Controls.DataGrid



Include Data Grid Styles

You must reference the data grid themes to include the additional styles that the data grid uses. You can do this by adding a <StyleInclude> element to the application (App.axaml file).

```c#
<Application.Styles>
    <FluentTheme />
    <StyleInclude Source="avares://Avalonia.Controls.DataGrid/Themes/Fluent.xaml"/>
</Application.Styles>
```

Пример с автогенерацией:
```c#
<DataGrid
        ItemsSource="{Binding #root.orderList}"
        AutoGenerateColumns="True"
        BorderThickness="1" BorderBrush="Black"
        GridLinesVisibility="All"
        IsReadOnly="True"
        >
        
    </DataGrid>
```

Пример с ручной разметкой:
```c#
<DataGrid Margin="20" ItemsSource="{Binding People}"
          //CanUserReorderColumns="True"
          //CanUserResizeColumns="True"
          //CanUserSortColumns="False"
          BorderThickness="1" BorderBrush="Gray"
          GridLinesVisibility="All"
          IsReadOnly="True"
          >
  <DataGrid.Columns>
     <DataGridTextColumn Header="First Name"  Binding="{Binding FirstName}"/>
     <DataGridTextColumn Header="Last Name" Binding="{Binding LastName}" />
  </DataGrid.Columns>
</DataGrid>
```

[↑ Содержание ↑](#содержание)

### <b>7. Открытие нового окна</b>

```c#
    var window = new OrganizerWindow();
    window.Show();
    // закрытие текущего
    Close();
```

[↑ Содержание ↑](#содержание)

### <b>8. Авторизация</b>

```c#
// кнопка авторизации
    private User currentUser = null;
    private string res = "";
    private async void Authorization_Button_OnClick(object? sender, RoutedEventArgs e)
    {
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
```

[↑ Содержание ↑](#содержание)

### <b>9. </b>