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
        private readonly db Database = new db();
        // GET api/values
        [EnableCors]
        [HttpGet]
        public ActionResult<IEnumerable<DRPenv>> Get()
        {
            List<DRPenv> values = Database.GetAll();
            return values;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<DRPenv> Get(int id)
        {
            return Database.GetID(id);
        }

        // POST api/values
        [HttpPost]
        public DRPenv Post(PostDRPenv values)
        {
            DRPenv drp = DRPenv.ConvertBack(values);
            Database.Insert(drp);
            return drp;
        }

    }
}
