using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Domain;

namespace Api.Controllers
{
    public class IngredientsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Ingredients
        public IQueryable<Ingredient> GetIngredientes()
        {
            return db.Ingredientes;
        }

        // GET: api/Ingredients/5
        [ResponseType(typeof(Ingredient))]
        public async Task<IHttpActionResult> GetIngredient(int id)
        {
            Ingredient ingredient = await db.Ingredientes.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return Ok(ingredient);
        }

        // PUT: api/Ingredients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIngredient(int id, Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingredient.IngredientId)
            {
                return BadRequest();
            }

            db.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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

        // POST: api/Ingredients
        [ResponseType(typeof(Ingredient))]
        public async Task<IHttpActionResult> PostIngredient(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ingredientes.Add(ingredient);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ingredient.IngredientId }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [ResponseType(typeof(Ingredient))]
        public async Task<IHttpActionResult> DeleteIngredient(int id)
        {
            Ingredient ingredient = await db.Ingredientes.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            db.Ingredientes.Remove(ingredient);
            await db.SaveChangesAsync();

            return Ok(ingredient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IngredientExists(int id)
        {
            return db.Ingredientes.Count(e => e.IngredientId == id) > 0;
        }
    }
}