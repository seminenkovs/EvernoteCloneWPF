using System.Collections.ObjectModel;
using EvernoteCloneWPF.Model;

namespace EvernoteCloneWPF.ViewModel;

public class NotesVM
{
    public ObservableCollection<Notebook> Notebooks { get; set; }
    public ObservableCollection<Note> Notes { get; set; }

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