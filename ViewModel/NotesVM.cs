using System.Collections.ObjectModel;
using EvernoteCloneWPF.Model;
using EvernoteCloneWPF.ViewModel.Commands;
using EvernoteCloneWPF.ViewModel.Helpers;

namespace EvernoteCloneWPF.ViewModel;

public class NotesVM
{
    private Notebook _selectNotebook;

    public ObservableCollection<Notebook> Notebooks { get; set; }
    public ObservableCollection<Note> Notes { get; set; }
    public NewNotebookCommand NewNotebookCommand { get; set; }
    public NewNoteCommand NewNoteCommand { get; set; }
    public Notebook SelectedNotebook
	{
		get { return _selectNotebook; }
        set
        {
            _selectNotebook = value;
            //TODO: get notes
        }
	}

    public NotesVM()
    {
        NewNotebookCommand = new NewNotebookCommand(this);
        NewNotebookCommand = new NewNotebookCommand(this);

        Notebooks = new ObservableCollection<Notebook>();
        Notes = new ObservableCollection<Note>();

        GetNotebooks();
    }

    public void CreateNotebook()
    {
        Notebook newNotebook = new Notebook()
        {
            Name = "New notebook"
        };

        DatabaseHelper.Insert(newNotebook);
    }

    public void CreateNote(int notebookId)
    {
        Note newNote = new Note()
        {
            NoteBookId = notebookId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Title = "New Note"
        };

        DatabaseHelper.Insert(newNote);
    }

    private void GetNotebooks()
    {
        var notebooks = DatabaseHelper.Read<Notebook>();

        Notebooks.Clear();

        foreach (var notebook in notebooks)
        {
            Notebooks.Add(notebook);
        }
    }
}