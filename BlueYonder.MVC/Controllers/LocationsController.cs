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
using BlueYonder.Model;

namespace BlueYonder.MVC.Controllers
{
    public class LocationsController : ApiController
    {
        private BlueYonderEntities db = new BlueYonderEntities();

        // GET: api/Locations
        public IQueryable<Locations> GetLocations()
        {
            return db.Locations;
        }

        // GET: api/Locations/5
        [ResponseType(typeof(Locations))]
        public IHttpActionResult GetLocations(int id)
        {
            Locations locations = db.Locations.Find(id);
            if (locations == null)
            {
                return NotFound();
            }

            return Ok(locations);
        }

        // PUT: api/Locations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocations(int id, Locations locations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locations.LocationId)
            {
                return BadRequest();
            }

            db.Entry(locations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationsExists(id))
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

        // POST: api/Locations
        [ResponseType(typeof(Locations))]
        public IHttpActionResult PostLocations(Locations locations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locations.Add(locations);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = locations.LocationId }, locations);
        }

        // DELETE: api/Locations/5
        [ResponseType(typeof(Locations))]
        public IHttpActionResult DeleteLocations(int id)
        {
            Locations locations = db.Locations.Find(id);
            if (locations == null)
            {
                return NotFound();
            }

            db.Locations.Remove(locations);
            db.SaveChanges();

            return Ok(locations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationsExists(int id)
        {
            return db.Locations.Count(e => e.LocationId == id) > 0;
        }
    }
}