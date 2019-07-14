using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DRPmodifierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Db Database = new Db();
        // GET api/values returns all
        [EnableCors]
        [HttpGet]
        public ActionResult<IEnumerable<DRPenv>> Get()
        {
            List<DRPenv> values = Database.GetAll();
            return values;
        }

        // GET api/values/id 
        [HttpGet("{id}")]
        public ActionResult<DRPenv> Get(int id)
        {
            return Database.GetID(id);
        }

        // POST api/values accepts DRPenv in a JSON form
        [HttpPost]
        public string Post(PostDRPenv values)
        {
            DRPenv drp = DRPenv.ConvertBack(values);
            if (Database.CheckInsert(drp))
            {
                Database.Insert(drp);
                return "Successfully Added!";
            }else
            {
                return "Failed to Add!";
            }
        }

    }
}
