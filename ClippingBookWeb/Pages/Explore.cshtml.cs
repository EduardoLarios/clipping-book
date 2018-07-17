using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClippingBookWeb.Models;
using MegaDriveWeb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MegaDriveWeb.Pages
{
    public class ExploreModel : PageModel
    {
        public FrontEndModel PageContents;

        public void OnGet()
        {
            Guid folderGuid = Guid.Empty;

            if (HttpContext.Request.Query.ContainsKey("Guid"))
            {
                folderGuid = Guid.Parse(HttpContext.Request.Query["Guid"].ToString());
            }

            if (GlobalState.Book.AllFolders.Any(folder => folder.Guid == folderGuid))
            {
                PageContents = new FrontEndModel(GlobalState.Book, GlobalState.Book.AllFolders.FirstOrDefault(folder => folder.Guid == folderGuid));
            }
            else
            {
                PageContents = new FrontEndModel(GlobalState.Book, GlobalState.Book.AllFolders.FirstOrDefault(folder => folder.Guid == GlobalState.Book.Root));
            }
        }

        public void OnPut()
        {

        }
    }
}