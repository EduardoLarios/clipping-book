using ClippingBookWeb;
using Newtonsoft.Json;
using System;
using System.IO;

namespace ClippingBookWeb
{
    public static class GlobalState
    {
        public static ClippingBook Book;

        public static void ReadOrNewBook()
        {
            Console.WriteLine("No ClippingBook has been loaded.");
            var defaultPath = Directory.GetCurrentDirectory() + "\\clippingbook.json";

            if (!File.Exists(defaultPath))
            {
                Book = new ClippingBook();
                WriteBook();
                return;
            }

            try
            {
                var fileStream = new FileStream(defaultPath, FileMode.Open);
                using (var reader = new StreamReader(fileStream))
                {
                    var json = reader.ReadToEnd();
                    var loadedBook = JsonConvert.DeserializeObject<ClippingBook>(json);
                    Book = loadedBook;
                    return;
                }
            }
            catch
            {
                Book = new ClippingBook();
                WriteBook();
                return;
            }
        }

        public static void WriteBook()
        {
            var defaultPath = Directory.GetCurrentDirectory() + "\\clippingbook.json";
            var fileStream = new FileStream(defaultPath, FileMode.Create);
            using (var writer = new StreamWriter(fileStream))
            {
                var json = JsonConvert.SerializeObject(Book);
                writer.Write(json);
                return;
            }
        }
    }
}
