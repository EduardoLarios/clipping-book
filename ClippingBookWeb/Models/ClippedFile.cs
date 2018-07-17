using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClippingBookWeb.Models
{
    [Serializable]
    public class ClippedFile
    {
        public readonly string Path;

        public readonly Guid Guid;

        public HashSet<Guid> Folders = new HashSet<Guid>();

        public HashSet<Guid> Tags = new HashSet<Guid>();

        internal ClippedFile(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(path);
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }

            Guid = Guid.NewGuid();
        }

        [JsonConstructor]
        internal ClippedFile(string path, Guid guid, HashSet<Guid> tags)
        {
            Path = path;
            Guid = guid;
            tags = Tags;
        }
        
        public void AssociateWith(ClippedFolder folder)
        {
            Folders.Add(folder.Guid);
            folder.Files.Add(Guid);
        }

        public void AssociateWith(ClippedFile file)
        {
            throw new InvalidOperationException();
        }

        public void AssociateWith(ClippedTag tag)
        {
            throw new InvalidOperationException();
        }
    }
}
