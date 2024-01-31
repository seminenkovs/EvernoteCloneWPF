using System.IO;
using SQLite;

namespace EvernoteCloneWPF.ViewModel.Helpers;

public class DatabaseHelper
{
    private static string _dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

    public static bool Insert<T>(T item)
    {
        bool result = false;

        using (SQLiteConnection connection = new SQLiteConnection(_dbFile))
        {
            connection.CreateTable<T>();
            int rows = connection.Insert(item);
            if (rows > 0)
            {
                result = true;
            }
        }

        return result;
    }

    public static bool Update<T>(T item)
    {
        bool result = false;

        using (SQLiteConnection connection = new SQLiteConnection(_dbFile))
        {
            connection.CreateTable<T>();
            int rows = connection.Update(item);
            if (rows > 0)
            {
                result = true;
            }
        }

        return result;
    }

    public static bool Delete<T>(T item)
    {
        bool result = false;

        using (SQLiteConnection connection = new SQLiteConnection(_dbFile))
        {
            connection.CreateTable<T>();
            int rows = connection.Delete(item);
            if (rows > 0)
            {
                result = true;
            }
        }

        return result;
    }

    public static List<T> Read<T>() where T : new()
    {
        List<T> items;

        using (SQLiteConnection connection = new SQLiteConnection(_dbFile))
        {
            connection.CreateTable<T>();
            items = connection.Table<T>().ToList();
        }

        return items;
    }
}