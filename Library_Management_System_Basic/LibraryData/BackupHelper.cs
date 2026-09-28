using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LibraryData
{
    public class BackupHelper
    {
        public bool BackupBooksToJson(List<Book> list, string path)
        {
            try
            {
                string json = JsonSerializer.Serialize(list);
                File.WriteAllText(path, json);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Book> LoadBooksFromJsonBackup(string path)
        {
            List<Book> list = new List<Book>();
            try
            {
                if (File.Exists(path) == false)
                {
                    throw new Exception("no file");
                }

                string text = File.ReadAllText(path);
                list = JsonSerializer.Deserialize<List<Book>>(text);

                return list;
            }
            catch (Exception e)
            {
                return list;
            }
        }
    }
}
