using SQLite;

namespace EvernoteCloneWPF.Model;

public class Notebook
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Indexed]
    public string UserId { get; set; }
    public string Name { get; set; }
}