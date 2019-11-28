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
using MVCFelicidadCrud.Models;

namespace MVCFelicidadCrud.Controllers
{
    public class alegriasController : ApiController
    {
        private BaseFelizEntities db = new BaseFelizEntities();

        // GET: api/alegrias
        public IQueryable<alegria> Getalegria()
        {
            return db.alegria;
        }

        // GET: api/alegrias/5
        [ResponseType(typeof(alegria))]
        public IHttpActionResult Getalegria(long id)
        {
            alegria alegria = db.alegria.Find(id);
            if (alegria == null)
            {
                return NotFound();
            }

            return Ok(alegria);
        }

        // PUT: api/alegrias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putalegria(long id, alegria alegria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alegria.idalegria)
            {
                return BadRequest();
            }

            db.Entry(alegria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!alegriaExists(id))
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

        // POST: api/alegrias
        [ResponseType(typeof(alegria))]
        public IHttpActionResult Postalegria(alegria alegria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.alegria.Add(alegria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = alegria.idalegria }, alegria);
        }

        // DELETE: api/alegrias/5
        [ResponseType(typeof(alegria))]
        public IHttpActionResult Deletealegria(long id)
        {
            alegria alegria = db.alegria.Find(id);
            if (alegria == null)
            {
                return NotFound();
            }

            db.alegria.Remove(alegria);
            db.SaveChanges();

            return Ok(alegria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool alegriaExists(long id)
        {
            return db.alegria.Count(e => e.idalegria == id) > 0;
        }
    }
}