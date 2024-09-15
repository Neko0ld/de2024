Шпора для себя

<b>1. Dapper</b>

Команды импорта:
- dotnet add package MySqlConnector
- dotnet add package Dapper

Пример
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

<b>2. Интерфейс</b>

```c#
public partial class MainWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void Invalidate()
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs("userList"));
    }
    ...
}
```

<b>3. MessageBox для avalonia</b>

Команда импорта:
- dotnet add package MessageBox.Avalonia

[Инструкция как пользоваться](https://github.com/AvaloniaCommunity/MessageBox.Avalonia)

<b> 4. База для списка</b>

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

