using System.Windows.Input;
using EvernoteCloneWPF.Model;

namespace EvernoteCloneWPF.ViewModel.Commands;

public class LoginCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public LoginVM ViewModel { get; set; }

    public LoginCommand(LoginVM vm)
    {
        ViewModel = vm;
    }

    public bool CanExecute(object? parameter)
    {
        User user = parameter as User;

        if (user == null)
        {
            return false;
        }
        if (string.IsNullOrEmpty(user.UserName))
        {
            return false;
        }
        if (string.IsNullOrEmpty(user.Password))
        {
            return false;
        }

        return true;
    }

    public void Execute(object? parameter)
    {
        ViewModel.Login();
    }
}