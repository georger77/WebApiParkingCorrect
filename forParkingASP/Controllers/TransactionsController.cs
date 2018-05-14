using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkingLibrary;

namespace forParkingASP.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        private Parking _db;
        public TransactionsController(Parking db)
        {
            this._db = db;

        }
        // GET api/transactions
        //Вивести Transactions.log
        [HttpGet]


        public JsonResult Get()
        {

            return Json(JsonConvert.SerializeObject(_db.ReturnLogString()));

            
                
        }

        // GET api/transactions/message
        //Вивести транзакції за останню хвилину
        //Вивести транзакції за останню хвилину по одній конкретній машині 

        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            
            if (id == "all")  return Json(JsonConvert.SerializeObject(_db.ReturnTransactionsPerLastMinute()));
            return Json(JsonConvert.SerializeObject(_db.ReturnTransactionsPerLastMinute(id)));
        }
        // PUT api/transactions/5
        //Поповнити баланс машини
        
        [HttpPut]
        public void Put(string id, [FromBody]double bal)
        {
            _db.IncreaseBalanceCarById(id, bal, true);
        }
        
       

    }
}