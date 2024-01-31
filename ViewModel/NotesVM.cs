using System.Collections.ObjectModel;
using EvernoteCloneWPF.Model;
using EvernoteCloneWPF.ViewModel.Commands;

namespace EvernoteCloneWPF.ViewModel;

public class NotesVM
{
    public ObservableCollection<Notebook> Notebooks { get; set; }
    public ObservableCollection<Note> Notes { get; set; }
    public NewNotebookCommand NewNotebookCommand { get; set; }
    public NewNoteCommand NewNoteCommand { get; set; }

    public NotesVM()
    {
        NewNotebookCommand = new NewNotebookCommand(this);
        NewNotebookCommand = new NewNotebookCommand(this);
    }

	private Notebook _selectNotebook;

	public Notebook SelectedNotebook
	{
		get { return _selectNotebook; }
        set
        {
            _selectNotebook = value;
            //TODO: get notes
        }
	}



}