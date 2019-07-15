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
        public ActionResult<string> Post(DRPenv values)
        {
            string result = Database.CheckInsert(values);
            if (result == "Success")
            {
                Database.Insert(values);
                return Ok("Successfully Added!");
            }else
            {
                return BadRequest(result);
            }
        }

    }
}
