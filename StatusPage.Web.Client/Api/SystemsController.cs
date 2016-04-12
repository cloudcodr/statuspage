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
using StatusPage.Web.Security;

namespace StatusPage.Web.Api
{
    [ApiAuthentication]
    [Authorize]
    public class SystemsController : ApiController
    {
        private StatusPageContext db = new StatusPageContext(true);

        // GET: api/SystemsApi
        public IQueryable<StatusPage.Data.Models.System> GetSystems()
        {
            return db.Systems;
        }

        // GET: api/SystemsApi/5
        [ResponseType(typeof(StatusPage.Data.Models.System))]
        public IHttpActionResult GetSystem(Guid id)
        {
            StatusPage.Data.Models.System system = db.Systems.Find(id);
            if (system == null)
            {
                return NotFound();
            }

            return Ok(system);
        }

        // PUT: api/SystemsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSystem(Guid id, StatusPage.Data.Models.System system)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != system.ID)
            {
                return BadRequest();
            }

            db.Entry(system).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemExists(id))
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

        // POST: api/SystemsApi
        [ResponseType(typeof(StatusPage.Data.Models.System))]
        public IHttpActionResult PostSystem(StatusPage.Data.Models.System system)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Systems.Add(system);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SystemExists(system.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = system.ID }, system);
        }

        // DELETE: api/SystemsApi/5
        [ResponseType(typeof(StatusPage.Data.Models.System))]
        public IHttpActionResult DeleteSystem(Guid id)
        {
            StatusPage.Data.Models.System system = db.Systems.Find(id);
            if (system == null)
            {
                return NotFound();
            }

            db.Systems.Remove(system);
            db.SaveChanges();

            return Ok(system);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SystemExists(Guid id)
        {
            return db.Systems.Count(e => e.ID == id) > 0;
        }
    }
}