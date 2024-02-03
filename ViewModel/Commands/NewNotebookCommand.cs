using System.Windows.Input;

namespace EvernoteCloneWPF.ViewModel.Commands;

public class NewNotebookCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public NotesVM VM { get; set; }

    public NewNotebookCommand(NotesVM vm)
    {
        VM = vm;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        //TODO: create new notebook
        VM.CreateNotebook();
    }
}