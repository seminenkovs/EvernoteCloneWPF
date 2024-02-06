using System.Windows.Input;

namespace EvernoteCloneWPF.ViewModel.Commands;

public class EditCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    public NotesVM ViewModel { get; set; }

    public EditCommand(NotesVM vm)
    {
        ViewModel = vm;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        ViewModel.StartEditing();
    }
}