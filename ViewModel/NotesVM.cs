using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EvernoteCloneWPF.Model;
using EvernoteCloneWPF.ViewModel.Commands;
using EvernoteCloneWPF.ViewModel.Helpers;

namespace EvernoteCloneWPF.ViewModel;

public class NotesVM : INotifyPropertyChanged
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
            OnPropertyChanged("SelectedNotebook");
            GetNotes();
        }
	}

    public event PropertyChangedEventHandler? PropertyChanged;

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

        GetNotebooks();
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

        GetNotes();
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

    private void GetNotes()
    {
        if (SelectedNotebook != null)
        {
            var notes = DatabaseHelper.Read<Note>()
                .Where(n => n.NoteBookId == SelectedNotebook.Id).ToList();

            Notes.Clear();

            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}