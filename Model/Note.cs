using SQLite;

namespace EvernoteCloneWPF.Model;

public class Note
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Indexed]
    public int NoteBookId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}
    public string FileLocation { get; set; }
}