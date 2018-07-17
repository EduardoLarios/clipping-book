using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClippingBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static MegaDriveWeb.ClippingBook;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MegaDriveWeb
{
    [Route("api/[controller]")]
    public class CRUD : Controller
    {
        /*
        // GET: api/<controller>
        [Route("folders")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */

        // GET api/<controller>/folders/<id>
        [Route("folders")]
        [HttpGet("{id}")]
        public string GetFolder(Guid id)
        {
            return "value";
        }

        // POST api/<controller>/folders/
        [Route("folders")]
        [HttpPost]
        public Guid PostFolder([FromBody]string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return Guid.Empty;
            }

            return GlobalState.Book.NewFolder(name);
        }

        // PUT api/<controller>/folders/<id>
        [Route("folders")]
        [HttpPut("{id}")]
        public bool PutFolder(Guid id, [FromBody]string serializedFolder)
        {
            try
            {
                ClippedFolder folder = JsonConvert.DeserializeObject<ClippedFolder>(serializedFolder);
                GlobalState.Book.AddFolder(folder);
            }
            catch
            {
                return false;
            }
            return true;
        }

        // GET api/<controller>/files/<id>
        [Route("files")]
        [HttpGet("{id}")]
        public string GetFile(int id)
        {
            return "value";
        }

        // POST api/<controller>/files/
        [Route("files")]
        [HttpPost]
        public Guid PostFile([FromBody]string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                return Guid.Empty;
            }

            return GlobalState.Book.NewFile(path);
        }

        // PUT api/<controller>/files/<id>
        [Route("files")]
        [HttpPut("{id}")]
        public bool PutFile(Guid id, [FromBody]string serializedFile)
        {
            try
            {
                ClippedFile file = JsonConvert.DeserializeObject<ClippedFile>(serializedFile);
                GlobalState.Book.AddFile(file);
            }
            catch
            {
                return false;
            }
            return true;
        }

        // GET api/<controller>/tags/<id>
        [Route("tags")]
        [HttpGet("{id}")]
        public string GetTag(int id)
        {
            return "value";
        }

        // POST api/<controller>/tags/
        [Route("tags")]
        [HttpPost]
        public Guid PostTag([FromBody]string json)
        {
            if (String.IsNullOrWhiteSpace(json))
            {
                return Guid.Empty;
            }

            try
            {
                Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                if (!result.ContainsKey("Name") || !result.ContainsKey("SortingRank"))
                {
                    return Guid.Empty;
                }

                return GlobalState.Book.NewTag(result["Name"], UInt16.Parse(result["SortingRank"]));
            }
            catch
            {
                return Guid.Empty;
            }
        }

        // PUT api/<controller>/tags/<id>
        [Route("tags")]
        [HttpPut("{id}")]
        public bool PutTag(Guid id, [FromBody]string serializedTag)
        {
            try
            {
                ClippedTag tag = JsonConvert.DeserializeObject<ClippedTag>(serializedTag);
                GlobalState.Book.AddTag(tag);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
