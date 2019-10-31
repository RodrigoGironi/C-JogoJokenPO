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
    public class JogosController : ApiController
    {
        private Rodrigo_MVCEntities db = new Rodrigo_MVCEntities();

        // GET api/Jogos
        public IQueryable<Jogos> GetJogos()
        {
            return db.Jogos;
        }

        // GET api/Jogos/5
        [ResponseType(typeof(Jogos))]
        public IHttpActionResult GetJogos(int id)
        {
            Jogos jogos = db.Jogos.Find(id);
            if (jogos == null)
            {
                return NotFound();
            }

            return Ok(jogos);
        }

        // PUT api/Jogos/5
        public IHttpActionResult PutJogos(int id, Jogos jogos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jogos.idJogo)
            {
                return BadRequest();
            }

            db.Entry(jogos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JogosExists(id))
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

        // POST api/Jogos
        [ResponseType(typeof(Jogos))]
        public IHttpActionResult PostJogos(Jogos jogos)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.Jogos.Add(jogos);

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateException)
            //{
            //    if (JogosExists(jogos.idJogo))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            db.Jogos.Add(jogos);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = jogos.idJogo }, jogos);
        }

        // DELETE api/Jogos/5
        [ResponseType(typeof(Jogos))]
        public IHttpActionResult DeleteJogos(int id)
        {
            Jogos jogos = db.Jogos.Find(id);
            if (jogos == null)
            {
                return NotFound();
            }

            db.Jogos.Remove(jogos);
            db.SaveChanges();

            return Ok(jogos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JogosExists(int id)
        {
            return db.Jogos.Count(e => e.idJogo == id) > 0;
        }
    }
}