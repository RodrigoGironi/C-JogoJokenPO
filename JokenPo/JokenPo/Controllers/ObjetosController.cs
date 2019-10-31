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
using JokenPo.Models;

namespace JokenPo.Controllers
{
    public class ObjetosController : ApiController
    {
        private Rodrigo_MVCEntities db = new Rodrigo_MVCEntities();

        // GET api/Objetos
        public IQueryable<Objetos> GetObjetos()
        {
            return db.Objetos;
        }

        // GET api/Objetos/5
        [ResponseType(typeof(Objetos))]
        public IHttpActionResult GetObjetos(int id)
        {
            Objetos objetos = db.Objetos.Find(id);
            if (objetos == null)
            {
                return NotFound();
            }

            return Ok(objetos);
        }

        // PUT api/Objetos/5
        public IHttpActionResult PutObjetos(int id, Objetos objetos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objetos.idObj)
            {
                return BadRequest();
            }

            db.Entry(objetos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjetosExists(id))
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

        // POST api/Objetos
        [ResponseType(typeof(Objetos))]
        public IHttpActionResult PostObjetos(Objetos objetos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Objetos.Add(objetos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = objetos.idObj }, objetos);
        }

        // DELETE api/Objetos/5
        [ResponseType(typeof(Objetos))]
        public IHttpActionResult DeleteObjetos(int id)
        {
            Objetos objetos = db.Objetos.Find(id);
            if (objetos == null)
            {
                return NotFound();
            }

            db.Objetos.Remove(objetos);
            db.SaveChanges();

            return Ok(objetos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ObjetosExists(int id)
        {
            return db.Objetos.Count(e => e.idObj == id) > 0;
        }
    }
}