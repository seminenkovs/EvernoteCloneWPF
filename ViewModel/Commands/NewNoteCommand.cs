using System.Windows.Input;
using EvernoteCloneWPF.Model;

namespace EvernoteCloneWPF.ViewModel.Commands;

public class NewNoteCommand : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
        
    }

    public NotesVM VM { get; set; }

    public NewNoteCommand(NotesVM vm)
    {
        VM = vm;
    }

    public bool CanExecute(object? parameter)
    {
        Notebook selectedNotebook = parameter as Notebook;
        
        return selectedNotebook != null ? true : false;
    }

    public void Execute(object? parameter)
    {
        Notebook selectedNotebook = parameter as Notebook;

        VM.CreateNote(selectedNotebook.Id);
    }
}