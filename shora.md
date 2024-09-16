Шпора для себя

Содержание:

- [1. Установка Avalonia](#1-установка-avalonia)
- [2. Установка Dapper](#2-dapper)
- [3. Интерфейс INotifyPropertyChanged](#3-интерфейс-inotifypropertychanged)
- [4. MessageBox для Avalonia](#4-messagebox-для-avalonia)
- [5. База List](#5-база-list)

### <b>1. Установка Avalonia</b>

Прописать в командную строку (win + r => cmd)
- dotnet new install Avalonia.Templates

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

### <b> 5. База List</b>

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

