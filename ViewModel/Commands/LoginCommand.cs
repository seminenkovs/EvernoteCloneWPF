using System.Windows.Input;

namespace EvernoteCloneWPF.ViewModel.Commands;

public class LoginCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public LoginVM VM { get; set; }

    public LoginCommand(LoginVM vm)
    {
        VM = vm;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        //TODO login functionality
    }
}