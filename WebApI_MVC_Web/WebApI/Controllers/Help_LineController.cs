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
    public class Help_LineController : ApiController
    {
        private DBMobileAppEntities db = new DBMobileAppEntities();

        // GET: api/Help_Line
        public IQueryable<Help_Line> GetHelp_Line()
        {
            return db.Help_Line;
        }

        // GET: api/Help_Line/5
        [ResponseType(typeof(Help_Line))]
        public IHttpActionResult GetHelp_Line(int id)
        {
            Help_Line help_Line = db.Help_Line.Find(id);
            if (help_Line == null)
            {
                return NotFound();
            }

            return Ok(help_Line);
        }

        // PUT: api/Help_Line/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHelp_Line(int id, Help_Line help_Line)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != help_Line.Help_Line_ID)
            {
                return BadRequest();
            }

            db.Entry(help_Line).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Help_LineExists(id))
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

        // POST: api/Help_Line
        [ResponseType(typeof(Help_Line))]
        public IHttpActionResult PostHelp_Line(Help_Line help_Line)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Help_Line.Add(help_Line);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = help_Line.Help_Line_ID }, help_Line);
        }

        // DELETE: api/Help_Line/5
        [ResponseType(typeof(Help_Line))]
        public IHttpActionResult DeleteHelp_Line(int id)
        {
            Help_Line help_Line = db.Help_Line.Find(id);
            if (help_Line == null)
            {
                return NotFound();
            }

            db.Help_Line.Remove(help_Line);
            db.SaveChanges();

            return Ok(help_Line);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Help_LineExists(int id)
        {
            return db.Help_Line.Count(e => e.Help_Line_ID == id) > 0;
        }
    }
}