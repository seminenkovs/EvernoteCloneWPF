using SQLite;

namespace EvernoteCloneWPF.Model;

public class Note : IHasId
{
    public string Id { get; set; }
    public string NoteBookId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}
    public string FileLocation { get; set; }
    
}