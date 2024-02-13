using System.IO;
using System.Net.Http;
using System.Text;
using EvernoteCloneWPF.Model;
using Newtonsoft.Json;
using SQLite;

namespace EvernoteCloneWPF.ViewModel.Helpers;

public class DatabaseHelper
{
    private static string _dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");
    private static string _dbPath = "https://notes-app-wpf-821ff-default-rtdb.firebaseio.com/";

    public static async Task<bool> Insert<T>(T item)
    {
        #region SQLiteConnection

        //bool result = false;

        //using (SQLiteConnection connection = new SQLiteConnection(_dbFile))
        //{
        //    connection.CreateTable<T>();
        //    int rows = connection.Insert(item);
        //    if (rows > 0)
        //    {
        //        result = true;
        //    }
        //}

        //return result;

        #endregion

        var jsonBody = JsonConvert.SerializeObject(item);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        using (var client = new HttpClient())
        {
           var result = await client.PostAsync(
               $"{_dbPath}{item.GetType().Name.ToLower()}.json", content);

           if (result.IsSuccessStatusCode)
           {
               return true;
           }
           else
           {
               return false;
           }
        }
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

    public static async Task<List<T>> Read<T>() where T : IHasId
    {
        #region SQLite

        //List<T> items;

        //using (SQLiteConnection connection = new SQLiteConnection(_dbFile))
        //{
        //    connection.CreateTable<T>();
        //    items = connection.Table<T>().ToList();
        //}

        //return items;

        #endregion

        using (var client = new HttpClient())
        {
            var result = await client.GetAsync(
                $"{_dbPath}{typeof(T).Name.ToLower()}.json");
            var jsonResult = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonResult);

                List<T> list = new List<T>();
                foreach (var o in objects)
                {
                    o.Value.Id = o.Key;
                    list.Add(o.Value);
                }

                return list;
            }
            else
            {
                return null;
            }
            
        }
    }
}