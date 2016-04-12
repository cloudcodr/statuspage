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
using StatusPage.Data.Models;

namespace StatusPage.Web.Api
{
    public class IncidentsController : ApiController
    {
        private StatusPageContext db = new StatusPageContext(true);

        // GET: api/Incidents
        public IQueryable<Incident> GetIncidents()
        {
            return db.Incidents;
        }

        // GET: api/Incidents/5
        [ResponseType(typeof(Incident))]
        public IHttpActionResult GetIncident(Guid id)
        {
            Incident incident = db.Incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }

            return Ok(incident);
        }

        // PUT: api/Incidents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIncident(Guid id, Incident incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != incident.ID)
            {
                return BadRequest();
            }

            db.Entry(incident).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentExists(id))
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

        // POST: api/Incidents
        [ResponseType(typeof(Incident))]
        public IHttpActionResult PostIncident(Incident incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Incidents.Add(incident);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (IncidentExists(incident.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = incident.ID }, incident);
        }

        // DELETE: api/Incidents/5
        [ResponseType(typeof(Incident))]
        public IHttpActionResult DeleteIncident(Guid id)
        {
            Incident incident = db.Incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }

            db.Incidents.Remove(incident);
            db.SaveChanges();

            return Ok(incident);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IncidentExists(Guid id)
        {
            return db.Incidents.Count(e => e.ID == id) > 0;
        }
    }
}