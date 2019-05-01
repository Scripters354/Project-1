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
    public class SymptomController : ApiController
    {
        private DBMobileAppEntities db = new DBMobileAppEntities();

        // GET: api/Symptom
        public IQueryable<Symptom_Sign> GetSymptom_Sign()
        {
            return db.Symptom_Sign;
        }

        // GET: api/Symptom/5
        [ResponseType(typeof(Symptom_Sign))]
        public IHttpActionResult GetSymptom_Sign(int id)
        {
            Symptom_Sign symptom_Sign = db.Symptom_Sign.Find(id);
            if (symptom_Sign == null)
            {
                return NotFound();
            }

            return Ok(symptom_Sign);
        }

        // PUT: api/Symptom/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSymptom_Sign(int id, Symptom_Sign symptom_Sign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != symptom_Sign.Symptom_Sign_ID)
            {
                return BadRequest();
            }

            db.Entry(symptom_Sign).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Symptom_SignExists(id))
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

        // POST: api/Symptom
        [ResponseType(typeof(Symptom_Sign))]
        public IHttpActionResult PostSymptom_Sign(Symptom_Sign symptom_Sign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Symptom_Sign.Add(symptom_Sign);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = symptom_Sign.Symptom_Sign_ID }, symptom_Sign);
        }

        // DELETE: api/Symptom/5
        [ResponseType(typeof(Symptom_Sign))]
        public IHttpActionResult DeleteSymptom_Sign(int id)
        {
            Symptom_Sign symptom_Sign = db.Symptom_Sign.Find(id);
            if (symptom_Sign == null)
            {
                return NotFound();
            }

            db.Symptom_Sign.Remove(symptom_Sign);
            db.SaveChanges();

            return Ok(symptom_Sign);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Symptom_SignExists(int id)
        {
            return db.Symptom_Sign.Count(e => e.Symptom_Sign_ID == id) > 0;
        }
    }
}