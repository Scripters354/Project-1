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
    public class StatisticsController : ApiController
    {
        private DBMobileAppEntities db = new DBMobileAppEntities();

        // GET: api/Statistics
        public IQueryable<Statistic> GetStatistics()
        {
            return db.Statistics;
        }

        // GET: api/Statistics/5
        [ResponseType(typeof(Statistic))]
        public IHttpActionResult GetStatistic(int id)
        {
            Statistic statistic = db.Statistics.Find(id);
            if (statistic == null)
            {
                return NotFound();
            }

            return Ok(statistic);
        }

        // PUT: api/Statistics/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStatistic(int id, Statistic statistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statistic.Statistic_ID)
            {
                return BadRequest();
            }

            db.Entry(statistic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatisticExists(id))
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

        // POST: api/Statistics
        [ResponseType(typeof(Statistic))]
        public IHttpActionResult PostStatistic(Statistic statistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Statistics.Add(statistic);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = statistic.Statistic_ID }, statistic);
        }

        // DELETE: api/Statistics/5
        [ResponseType(typeof(Statistic))]
        public IHttpActionResult DeleteStatistic(int id)
        {
            Statistic statistic = db.Statistics.Find(id);
            if (statistic == null)
            {
                return NotFound();
            }

            db.Statistics.Remove(statistic);
            db.SaveChanges();

            return Ok(statistic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatisticExists(int id)
        {
            return db.Statistics.Count(e => e.Statistic_ID == id) > 0;
        }
    }
}