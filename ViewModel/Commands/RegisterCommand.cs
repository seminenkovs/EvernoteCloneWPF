using System.Windows.Input;
using EvernoteCloneWPF.Model;

namespace EvernoteCloneWPF.ViewModel.Commands;

public class RegisterCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public LoginVM ViewModel { get; set; }

    public RegisterCommand(LoginVM viewModel)
    {
        ViewModel = viewModel;
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
        if (string.IsNullOrEmpty(user.ConfirmPassword))
        {
            return false;
        }
        if (user.Password != user.ConfirmPassword)
        {
            return false;
        }

        return true;
    }

    public void Execute(object? parameter)
    {
        ViewModel.Register();
    }
}