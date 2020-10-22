using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class WeightApiController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public WeightApiController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetValue()
        {
            var value = _context.Detail.ToList();
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }
        public IHttpActionResult GetValue(int id)
        {
            var value = _context.Detail.SingleOrDefault(c => c.id == id);
            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }
        //Post /api/customerapi
        [HttpPost]
        public IHttpActionResult CreateData(Details detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model data is invalid");
            }

            _context.Detail.Add(detail);
            _context.SaveChanges();
            return Ok(detail);
        }
        //Put /api/customerapi/1
        [HttpPut]
        public IHttpActionResult UpdateData(int id, Details detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model data is invalid");
            }
            var proInDb = _context.Detail.SingleOrDefault(c => c.id == id);
            if (proInDb == null)
            {
                return NotFound();
            }

            proInDb.date = detail.date;
            proInDb.weight = detail.weight;
            proInDb.bodyfat = detail.bodyfat;

            _context.SaveChanges();
            return Ok();
        }
        //Delete /api/customerapi/1

        public IHttpActionResult DeleteData(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a Valid Customer id");
            }
            var proInDb = _context.Detail.SingleOrDefault(c => c.id == id);
            if (proInDb == null)
            {
                return NotFound();
            }

            _context.Detail.Remove(proInDb);
            _context.SaveChanges();
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}

