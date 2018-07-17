using ClippingBookWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MegaDriveWeb
{
    [Serializable]
    public class ClippingBook
    {

        public UInt64 Version;

        public Guid Root;

        public List<ClippedFolder> AllFolders = new List<ClippedFolder>();

        public List<ClippedFile> AllFiles = new List<ClippedFile>();

        public List<ClippedTag> AllTags = new List<ClippedTag>();

        public Guid NewFolder(string name)
        {
            ClippedFolder newFolder = new ClippedFolder(name);
            AllFolders.Add(newFolder);
            return newFolder.Guid;
        }

        public Guid NewFile(string path)
        {
            ClippedFile newFile = new ClippedFile(path);
            AllFiles.Add(newFile);
            return newFile.Guid;
        }

        public Guid NewTag(string name, UInt16 rank)
        {
            ClippedTag newTag = new ClippedTag(name, rank);
            AllTags.Add(newTag);
            return newTag.Guid;
        }

        public void AddFolder(ClippedFolder newFolder)
        {
            if (newFolder == null)
            {
                throw new ArgumentNullException(nameof(newFolder));
            }
            if (newFolder.Folders.Any(guid => !AllFolders.Any(folder => folder.Guid == guid)))
            {
                throw new InvalidOperationException();
            }
            if (newFolder.Files.Any(guid => !AllFiles.Any(file => file.Guid == guid)))
            {
                throw new InvalidOperationException();
            }
            if (newFolder.Tags.Any(guid => !AllTags.Any(ele => ele.Guid == guid)))
            {
                throw new InvalidOperationException();
            }

            AllFolders.Add(newFolder);
            
            foreach (Guid folderGuid in newFolder.Folders)
            {
                ClippedFolder folder = AllFolders.FirstOrDefault(x => x.Guid == folderGuid);
                newFolder.AssociateWith(folder);
            }

            foreach (Guid fileGuid in newFolder.Files)
            {
                ClippedFile file = AllFiles.FirstOrDefault(x => x.Guid == fileGuid);
                newFolder.AssociateWith(file);
            }

            foreach (Guid tagGuid in newFolder.Tags)
            {
                ClippedTag tag = AllTags.FirstOrDefault(x => x.Guid == tagGuid);
                newFolder.AssociateWith(tag);
            }
        }

        public void AddFile(ClippedFile newFile)
        {
            if (newFile == null)
            {
                throw new ArgumentNullException(nameof(newFile));
            }
            if (newFile.Folders.Any(guid => !AllFolders.Any(folder => folder.Guid == guid)))
            {
                throw new InvalidOperationException();
            }
            if (newFile.Tags.Any(guid => !AllTags.Any(ele => ele.Guid == guid)))
            {
                throw new InvalidOperationException();
            }

            AllFiles.Add(newFile);

            foreach (Guid folderGuid in newFile.Folders)
            {
                ClippedFolder folder = AllFolders.FirstOrDefault(x => x.Guid == folderGuid);
                newFile.AssociateWith(folder);
            }

            foreach (Guid tagGuid in newFile.Tags)
            {
                ClippedTag tag = AllTags.FirstOrDefault(x => x.Guid == tagGuid);
                newFile.AssociateWith(tag);
            }
        }

        public void AddTag(ClippedTag newTag)
        {
            if (newTag == null)
            {
                throw new ArgumentNullException(nameof(newTag));
            }
            if (newTag.Folders.Any(guid => !AllFolders.Any(folder => folder.Guid == guid)))
            {
                throw new InvalidOperationException();
            }
            if (newTag.Files.Any(guid => !AllFiles.Any(file => file.Guid == guid)))
            {
                throw new InvalidOperationException();
            }

            AllTags.Add(newTag);

            foreach (Guid folderGuid in newTag.Folders)
            {
                ClippedFolder folder = AllFolders.FirstOrDefault(x => x.Guid == folderGuid);
                newTag.AssociateWith(folder);
            }

            foreach (Guid fileGuid in newTag.Files)
            {
                ClippedFile file = AllFiles.FirstOrDefault(x => x.Guid == fileGuid);
                newTag.AssociateWith(file);
            }
        }

        public void RemoveFolder(ClippedFolder target)
        {
            // Unassign/disassociate other Folders from this Folder
            foreach (Guid folderGuid in target.Folders)
            {
                ClippedFolder folder = AllFolders.First(x => x.Guid == folderGuid);
                folder.Folders.Remove(target.Guid);
            }

            // Unassign/disassociate other Files from this Folder
            foreach (Guid fileGuid in target.Files)
            {
                ClippedFile file = AllFiles.First(x => x.Guid == fileGuid);
                file.Folders.Remove(target.Guid);
            }

            // Unassign/disassociate other Tags from this Folder
            foreach (Guid tagGuid in target.Tags)
            {
                ClippedTag tag = AllTags.First(x => x.Guid == tagGuid);
                tag.Folders.Remove(target.Guid);
            }

            AllFolders.Remove(target);
        }

        public void RemoveFile(ClippedFile target)
        {
            // Unassign / disassociate other Folders from this File
            foreach (Guid folderGuid in target.Folders)
            {
                ClippedFolder folder = AllFolders.First(x => x.Guid == folderGuid);
                folder.Folders.Remove(target.Guid);
            }

            // Unassign/disassociate other Tags from this Folder
            foreach (Guid tagGuid in target.Tags)
            {
                ClippedTag tag = AllTags.First(x => x.Guid == tagGuid);
                tag.Folders.Remove(target.Guid);
            }

            AllFiles.Remove(target);
        }

        public void RemoveTag(ClippedTag target)
        {
            // Unassign/disassociate other Folders from this Folder
            foreach (Guid folderGuid in target.Folders)
            {
                ClippedFolder folder = AllFolders.First(x => x.Guid == folderGuid);
                folder.Folders.Remove(target.Guid);
            }

            // Unassign/disassociate other Files from this Folder
            foreach (Guid fileGuid in target.Files)
            {
                ClippedFile file = AllFiles.First(x => x.Guid == fileGuid);
                file.Folders.Remove(target.Guid);
            }

            AllTags.Remove(target);
        }

        public ClippingBook()
        {
            Root = NewFolder("ClippingBook Root");
        }
    }
}


