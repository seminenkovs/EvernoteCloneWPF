using SQLite;

namespace EvernoteCloneWPF.Model;

public class Notebook : IHasId
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
}