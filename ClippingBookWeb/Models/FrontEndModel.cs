using MegaDriveWeb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClippingBookWeb.Models
{
    public class FrontEndModel
    {
        public class FrontEndContent
        {
            public readonly string Text;

            public readonly Guid Guid;

            public readonly List<string> Tags = new List<string>();

            public FrontEndContent(string text, Guid guid, List<string> tags)
            {
                if (String.IsNullOrWhiteSpace(text))
                {
                    throw new ArgumentNullException(nameof(text));
                }
                Text = text;

                if (guid == null)
                {
                    throw new ArgumentNullException(nameof(guid));
                }
                Guid = guid;

                Tags = tags ?? throw new ArgumentNullException(nameof(tags));
            }
        }

        public readonly string Title;

        public List<string> Tags = new List<string>();

        public List<FrontEndContent> Contents = new List<FrontEndContent>();

        public readonly ClippingBook Book;

        public FrontEndModel(ClippingBook book, ClippedFolder folder)
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));
            if (folder == null)
            {
                throw new ArgumentNullException(nameof(folder));
            }

            Title = folder.Name;
            Tags = folder.Tags.Select(tagGuid => Book.AllTags.First(tag => tag.Guid == tagGuid).Name).ToList();

            foreach (var folderGuid in folder.Folders)
            {
                var otherFolder = Book.AllFolders.First(ff => ff.Guid == folderGuid);
                Contents.Add(new FrontEndContent
                (
                    otherFolder.Name,
                    otherFolder.Guid,
                    otherFolder.Tags.Select(tagGuid => Book.AllTags.First(tag => tag.Guid == tagGuid).Name).ToList()
                ));
            }
        }
    }
}
