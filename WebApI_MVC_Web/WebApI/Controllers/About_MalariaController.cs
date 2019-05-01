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
    public class About_MalariaController : ApiController
    {
        private DBMobileAppEntities db = new DBMobileAppEntities();

        // GET: api/About_Malaria
        public IQueryable<About_Malaria> GetAbout_Malaria()
        {
            return db.About_Malaria;
        }

        // GET: api/About_Malaria/5
        [ResponseType(typeof(About_Malaria))]
        public IHttpActionResult GetAbout_Malaria(int id)
        {
            About_Malaria about_Malaria = db.About_Malaria.Find(id);
            if (about_Malaria == null)
            {
                return NotFound();
            }

            return Ok(about_Malaria);
        }

        // PUT: api/About_Malaria/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAbout_Malaria(int id, About_Malaria about_Malaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != about_Malaria.About_Malaria_ID)
            {
                return BadRequest();
            }

            db.Entry(about_Malaria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!About_MalariaExists(id))
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

        // POST: api/About_Malaria
        [ResponseType(typeof(About_Malaria))]
        public IHttpActionResult PostAbout_Malaria(About_Malaria about_Malaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.About_Malaria.Add(about_Malaria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = about_Malaria.About_Malaria_ID }, about_Malaria);
        }

        // DELETE: api/About_Malaria/5
        [ResponseType(typeof(About_Malaria))]
        public IHttpActionResult DeleteAbout_Malaria(int id)
        {
            About_Malaria about_Malaria = db.About_Malaria.Find(id);
            if (about_Malaria == null)
            {
                return NotFound();
            }

            db.About_Malaria.Remove(about_Malaria);
            db.SaveChanges();

            return Ok(about_Malaria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool About_MalariaExists(int id)
        {
            return db.About_Malaria.Count(e => e.About_Malaria_ID == id) > 0;
        }
    }
}