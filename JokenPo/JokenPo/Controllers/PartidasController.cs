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
    public class PartidasController : ApiController
    {
        private Rodrigo_MVCEntities db = new Rodrigo_MVCEntities();

        // GET api/Partidas
        public IQueryable<Partida> GetPartida()
        {
            return db.Partida;
        }

        // GET api/Partidas/5
        [ResponseType(typeof(Partida))]
        public IHttpActionResult GetPartida(int id)
        {
            Partida partida = db.Partida.Find(id);
            if (partida == null)
            {
                return NotFound();
            }

            return Ok(partida);
        }

        // PUT api/Partidas/5
        public IHttpActionResult PutPartida(int id, Partida partida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != partida.idPartida)
            {
                return BadRequest();
            }

            db.Entry(partida).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartidaExists(id))
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

        // POST api/Partidas
        [ResponseType(typeof(Partida))]
        public IHttpActionResult PostPartida(Partida partida)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.Partida.Add(partida);

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateException)
            //{
            //    if (PartidaExists(partida.idPartida))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            db.Partida.Add(partida);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = partida.idPartida }, partida);
        }

        // DELETE api/Partidas/5
        [ResponseType(typeof(Partida))]
        public IHttpActionResult DeletePartida(int id)
        {
            Partida partida = db.Partida.Find(id);
            if (partida == null)
            {
                return NotFound();
            }

            db.Partida.Remove(partida);
            db.SaveChanges();

            return Ok(partida);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PartidaExists(int id)
        {
            return db.Partida.Count(e => e.idPartida == id) > 0;
        }
    }
}