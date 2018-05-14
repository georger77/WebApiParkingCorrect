using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingLibrary;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace forParkingASP.Controllers.API
{
   
    [Route("api/[controller]")]
    public class CarController : Controller
    {

        private Parking _db;
        public CarController(Parking db)
        {
            this._db = db;
            
        }
        // GET api/car
        //Список всіх машин
        [HttpGet]
        public JsonResult Get()
        {

            //   JsonConvert.DeserializeObject<List<Car>>(;
            
            return Json(JsonConvert.SerializeObject(_db._cars));

        }

        // GET api/cars/AA1111
        //Деталі по одній машині
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            Car car = _db._cars.FirstOrDefault(x => x.Id == id);
            if (car == null)
                return null;
           
            return Json(JsonConvert.SerializeObject(car));
        }

        // POST api/car
        //Додати машину 
        [HttpPost]
        public IActionResult Post([FromBody]Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }

            if (car.Id.Length == 6 &&
                                 car.Id[0] >= 65 &&  car.Id[0] < 90 &&
                                 car.Id[1] >= 65 &&  car.Id[1] < 90 &&
                                  car.Id[2] < 57 &&  car.Id[2] >= 48 &&
                                  car.Id[3] < 57 &&  car.Id[3] >= 48 &&
                                  car.Id[4] < 57 &&  car.Id[4] >= 48 &&
                                  car.Id[5] < 57 &&  car.Id[5] >= 48 &&
                                  car.CarType>=0 && (int)car.CarType <4 &&
                                  car.Balance>=0
                                 )

                _db.AddCar(car);
            return Ok();
        }

        // DELETE api/car/AA1111
        //Видалити машину
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _db.RemoveCarById(id);
            return Ok();

        }
    }
}
