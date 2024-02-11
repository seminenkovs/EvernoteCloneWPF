using System.Windows.Input;

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
        return true;
    }

    public void Execute(object? parameter)
    {
        ViewModel.Register();
    }
}