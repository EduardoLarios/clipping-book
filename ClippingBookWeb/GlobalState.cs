using MegaDriveWeb;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDriveWeb
{
    public static class GlobalState
    {
        public static ClippingBook Book;

        public static void ReadOrNewBook()
        {
            Console.WriteLine("No ClippingBook has been loaded.");
            string defaultPath = Directory.GetCurrentDirectory().ToString() + "\\clippingbook.json";

            if (!File.Exists(defaultPath))
            {
                Book = new ClippingBook();
                WriteBook();
                return;
            }

            try
            {
                FileStream fileStream = new FileStream(defaultPath, FileMode.Open);
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string json = reader.ReadToEnd();
                    ClippingBook loadedBook = JsonConvert.DeserializeObject<ClippingBook>(json);
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
            string defaultPath = Directory.GetCurrentDirectory().ToString() + "\\megadrive.json";
            FileStream fileStream = new FileStream(defaultPath, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                var json = JsonConvert.SerializeObject(Book);
                writer.Write(json);
                return;
            }
        }
    }
}
