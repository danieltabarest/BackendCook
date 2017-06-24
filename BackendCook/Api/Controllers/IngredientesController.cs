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
using Domain;

namespace Api.Controllers
{
    [Authorize]
    public class IngredientesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Ingredientes
        public IQueryable<Ingredient> GetIngredientes()
        {
            return db.Ingredientes;
        }

        // GET: api/Ingredientes/5
        [ResponseType(typeof(Ingredient))]
        public IHttpActionResult GetIngrediente(int id)
        {
            Ingredient ingrediente = db.Ingredientes.Find(id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return Ok(ingrediente);
        }

        // PUT: api/Ingredientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIngrediente(int id, Ingredient ingrediente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingrediente.IngredientId)
            {
                return BadRequest();
            }

            db.Entry(ingrediente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredienteExists(id))
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

        // POST: api/Ingredientes
        [ResponseType(typeof(Ingredient))]
        public IHttpActionResult PostIngrediente(Ingredient ingrediente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ingredientes.Add(ingrediente);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ingrediente.IngredientId }, ingrediente);
        }

        // DELETE: api/Ingredientes/5
        [ResponseType(typeof(Ingredient))]
        public IHttpActionResult DeleteIngrediente(int id)
        {
            Ingredient ingrediente = db.Ingredientes.Find(id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            db.Ingredientes.Remove(ingrediente);
            db.SaveChanges();

            return Ok(ingrediente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IngredienteExists(int id)
        {
            return db.Ingredientes.Count(e => e.IngredientId == id) > 0;
        }
    }
}