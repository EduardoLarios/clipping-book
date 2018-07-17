using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClippingBookWeb.Models
{
    [Serializable]
    public class ClippedTag : IComparable<ClippedTag>
    {
        public readonly string Name;

        public readonly Guid Guid;

        // NOTE(Gooseheaded): This SortingRank is poorly done, it should be "static" somehow.
        public readonly UInt16 SortingRank;

        public List<Guid> Files = new List<Guid>();

        public List<Guid> Folders = new List<Guid>();

        public int CompareTo(ClippedTag other)
        {
            return this.SortingRank - other.SortingRank;
        }

        internal ClippedTag(string name, UInt16 rank)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(name);
            }

            Name = name;
            Guid = Guid.NewGuid();
            SortingRank = rank;
        }

        [JsonConstructor]
        internal ClippedTag(string name, Guid guid, UInt16 sortingRank, List<Guid> files, List<Guid> folders)
        {
            Name = name;
            Guid = guid;
            SortingRank = sortingRank;
            Files = files;
            Folders = folders;
        }
        
        public void AssociateWith(ClippedFolder folder)
        {
            Folders.Add(folder.Guid);
            folder.Tags.Add(Guid);
        }

        public void AssociateWith(ClippedFile file)
        {
            Files.Add(file.Guid);
            file.Tags.Add(Guid);
        }

        public void AssociateWith(ClippedTag tag)
        {
            throw new InvalidOperationException();
        }
    }
}
