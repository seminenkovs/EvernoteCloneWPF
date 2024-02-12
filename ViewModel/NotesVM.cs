using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EvernoteCloneWPF.Model;
using EvernoteCloneWPF.ViewModel.Commands;
using EvernoteCloneWPF.ViewModel.Helpers;

namespace EvernoteCloneWPF.ViewModel;

public class NotesVM : INotifyPropertyChanged
{
    public ObservableCollection<Notebook> Notebooks { get; set; }
    public ObservableCollection<Note> Notes { get; set; }
    public NewNotebookCommand NewNotebookCommand { get; set; }
    public NewNoteCommand NewNoteCommand { get; set; }
    public EditCommand EditCommand { get; set; }
    public EndEditingCommand EndEditingCommand { get; set; }

    private Notebook _selectedNotebook;
    public Notebook SelectedNotebook
	{
		get { return _selectedNotebook; }
        set
        {
            _selectedNotebook = value;
            OnPropertyChanged("SelectedNotebook");
            GetNotes();
        }
	}

    private Note _selectedNote;

    public Note SelectedNote
    {
        get { return _selectedNote; }
        set
        {
            _selectedNote = value;
            OnPropertyChanged("SelectedNote");
            SelectedNoteChanged?.Invoke(this, new EventArgs());
        }
    }

    private Visibility _isVisible;

    public Visibility  IsVisible
    {
        get { return _isVisible; }
        set
        {
            _isVisible = value;
            OnPropertyChanged("IsVisible");
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler SelectedNoteChanged;

    public NotesVM()
    {
        NewNoteCommand = new NewNoteCommand(this);
        NewNotebookCommand = new NewNotebookCommand(this);
        EditCommand = new EditCommand(this);
        EndEditingCommand = new EndEditingCommand(this);

        Notebooks = new ObservableCollection<Notebook>();
        Notes = new ObservableCollection<Note>();

        IsVisible = Visibility.Collapsed;

        GetNotebooks();
    }

    public async void CreateNotebook()
    {
        Notebook newNotebook = new Notebook()
        {
            Name = "Notebook",
            UserId = App.UserId
        };

        await DatabaseHelper.Insert(newNotebook);

        GetNotebooks();
    }

    public async void CreateNote(int notebookId)
    {
        Note newNote = new Note()
        {
            NoteBookId = notebookId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Title = $"Note for {DateTime.Now.ToString()}"
        };

        await DatabaseHelper.Insert(newNote);

        GetNotes();
    }

    public void GetNotebooks()
    {
        var notebooks = DatabaseHelper.Read<Notebook>()
            .Where(n => n.UserId == App.UserId).ToList();

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

    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void StartEditing()
    {
        IsVisible = Visibility.Visible;
    }

    public void StopEditing(Notebook notebook)
    {
        IsVisible = Visibility.Collapsed;
        DatabaseHelper.Update(notebook);
        GetNotebooks();
    }
}