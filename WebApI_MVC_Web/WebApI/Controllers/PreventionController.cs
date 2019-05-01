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
    public class PreventionController : ApiController
    {
        private DBMobileAppEntities db = new DBMobileAppEntities();

        // GET: api/Prevention
        public IQueryable<Prevention> GetPreventions()
        {
            return db.Preventions;
        }

        // GET: api/Prevention/5
        [ResponseType(typeof(Prevention))]
        public IHttpActionResult GetPrevention(int id)
        {
            Prevention prevention = db.Preventions.Find(id);
            if (prevention == null)
            {
                return NotFound();
            }

            return Ok(prevention);
        }

        // PUT: api/Prevention/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrevention(int id, Prevention prevention)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prevention.Prevention_ID)
            {
                return BadRequest();
            }

            db.Entry(prevention).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreventionExists(id))
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

        // POST: api/Prevention
        [ResponseType(typeof(Prevention))]
        public IHttpActionResult PostPrevention(Prevention prevention)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Preventions.Add(prevention);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = prevention.Prevention_ID }, prevention);
        }

        // DELETE: api/Prevention/5
        [ResponseType(typeof(Prevention))]
        public IHttpActionResult DeletePrevention(int id)
        {
            Prevention prevention = db.Preventions.Find(id);
            if (prevention == null)
            {
                return NotFound();
            }

            db.Preventions.Remove(prevention);
            db.SaveChanges();

            return Ok(prevention);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PreventionExists(int id)
        {
            return db.Preventions.Count(e => e.Prevention_ID == id) > 0;
        }
    }
}