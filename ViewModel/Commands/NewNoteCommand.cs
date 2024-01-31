using System.Windows.Input;

namespace EvernoteCloneWPF.ViewModel.Commands;

public class NewNoteCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public NotesVM VM { get; set; }

    public NewNoteCommand(NotesVM vm)
    {
        VM = vm;
    }
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        //TODO: create new note
    }
}