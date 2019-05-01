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
    public class CauseController : ApiController
    {
        private DBMobileAppEntities db = new DBMobileAppEntities();

        // GET: api/Cause
        public IQueryable<Cause> GetCauses()
        {
            return db.Causes;
        }

        // GET: api/Cause/5
        [ResponseType(typeof(Cause))]
        public IHttpActionResult GetCause(int id)
        {
            Cause cause = db.Causes.Find(id);
            if (cause == null)
            {
                return NotFound();
            }

            return Ok(cause);
        }

        // PUT: api/Cause/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCause(int id, Cause cause)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cause.Cause_ID)
            {
                return BadRequest();
            }

            db.Entry(cause).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CauseExists(id))
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

        // POST: api/Cause
        [ResponseType(typeof(Cause))]
        public IHttpActionResult PostCause(Cause cause)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Causes.Add(cause);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cause.Cause_ID }, cause);
        }

        // DELETE: api/Cause/5
        [ResponseType(typeof(Cause))]
        public IHttpActionResult DeleteCause(int id)
        {
            Cause cause = db.Causes.Find(id);
            if (cause == null)
            {
                return NotFound();
            }

            db.Causes.Remove(cause);
            db.SaveChanges();

            return Ok(cause);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CauseExists(int id)
        {
            return db.Causes.Count(e => e.Cause_ID == id) > 0;
        }
    }
}