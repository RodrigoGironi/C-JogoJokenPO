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
    public class JogadorController : ApiController
    {
        private Rodrigo_MVCEntities db = new Rodrigo_MVCEntities();

        // GET api/Jogador
        public IQueryable<Jogador> GetJogador()
        {
            return db.Jogador;
        }

        // GET api/Jogador/5
        [ResponseType(typeof(Jogador))]
        public IHttpActionResult GetJogador(int id)
        {
            Jogador jogador = db.Jogador.Find(id);
            if (jogador == null)
            {
                return NotFound();
            }

            return Ok(jogador);
        }

        // PUT api/Jogador/5
        public IHttpActionResult PutJogador(int id, Jogador jogador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jogador.id)
            {
                return BadRequest();
            }

            db.Entry(jogador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JogadorExists(id))
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

        // POST api/Jogador
        [ResponseType(typeof(Jogador))]
        public IHttpActionResult PostJogador(Jogador jogador)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.Jogador.Add(jogador);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jogador.id }, jogador);
        }

        // DELETE api/Jogador/5
        [ResponseType(typeof(Jogador))]
        public IHttpActionResult DeleteJogador(int id)
        {
            Jogador jogador = db.Jogador.Find(id);
            if (jogador == null)
            {
                return NotFound();
            }

            db.Jogador.Remove(jogador);
            db.SaveChanges();

            return Ok(jogador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JogadorExists(int id)
        {
            return db.Jogador.Count(e => e.id == id) > 0;
        }
    }
}