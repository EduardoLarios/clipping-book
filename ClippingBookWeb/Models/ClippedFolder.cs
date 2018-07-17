using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClippingBookWeb.Models
{
    [Serializable]
    public class ClippedFolder
    {
        public readonly string Name;

        public readonly Guid Guid;

        public HashSet<Guid> Folders = new HashSet<Guid>();

        public HashSet<Guid> Files = new HashSet<Guid>();

        public HashSet<Guid> Tags = new HashSet<Guid>();

        internal ClippedFolder(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(name);
            }

            Name = name;
            Guid = Guid.NewGuid();
        }

        [JsonConstructor]
        internal ClippedFolder(string name, Guid guid, HashSet<Guid> folders, HashSet<Guid> files, HashSet<Guid> tags)
        {
            Name = name;
            Guid = guid;
            Folders = folders;
            Files = files;
            Tags = tags;
        }

        public void AssociateWith(ClippedFolder folder)
        {
            Folders.Add(folder.Guid);
            folder.Folders.Add(Guid);
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
