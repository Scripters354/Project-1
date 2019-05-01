using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApI.Models;

namespace WebApI.Controllers
{
    public class Risk_ZoneController : ApiController
    {
        private DBMobileAppEntities db = new DBMobileAppEntities();

        // GET: api/Risk_Zone
        public IQueryable<Malaria_Risk_Zone> GetMalaria_Risk_Zone()
        {
            return db.Malaria_Risk_Zone;
        }

        // GET: api/Risk_Zone/5
        [ResponseType(typeof(Malaria_Risk_Zone))]
        public IHttpActionResult GetMalaria_Risk_Zone(int id)
        {
            Malaria_Risk_Zone malaria_Risk_Zone = db.Malaria_Risk_Zone.Find(id);
            if (malaria_Risk_Zone == null)
            {
                return NotFound();
            }

            return Ok(malaria_Risk_Zone);
        }

        // PUT: api/Risk_Zone/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMalaria_Risk_Zone(int id, Malaria_Risk_Zone malaria_Risk_Zone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != malaria_Risk_Zone.Malaria_Risk_Zone_ID)
            {
                return BadRequest();
            }

            db.Entry(malaria_Risk_Zone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Malaria_Risk_ZoneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Risk_Zone
        [ResponseType(typeof(Malaria_Risk_Zone))]
        public IHttpActionResult PostMalaria_Risk_Zone(Malaria_Risk_Zone malaria_Risk_Zone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Malaria_Risk_Zone.Add(malaria_Risk_Zone);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = malaria_Risk_Zone.Malaria_Risk_Zone_ID }, malaria_Risk_Zone);
        }

        // DELETE: api/Risk_Zone/5
        [ResponseType(typeof(Malaria_Risk_Zone))]
        public IHttpActionResult DeleteMalaria_Risk_Zone(int id)
        {
            Malaria_Risk_Zone malaria_Risk_Zone = db.Malaria_Risk_Zone.Find(id);
            if (malaria_Risk_Zone == null)
            {
                return NotFound();
            }

            db.Malaria_Risk_Zone.Remove(malaria_Risk_Zone);
            db.SaveChanges();

            return Ok(malaria_Risk_Zone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Malaria_Risk_ZoneExists(int id)
        {
            return db.Malaria_Risk_Zone.Count(e => e.Malaria_Risk_Zone_ID == id) > 0;
        }
    }
}