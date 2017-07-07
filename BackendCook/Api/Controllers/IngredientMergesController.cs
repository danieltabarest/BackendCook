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
    public class IngredientMergesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/IngredientMerges
        public IQueryable<IngredientMerge> GetIngredientMerge()
        {
            return db.IngredientMerge;
        }

        // GET: api/IngredientMerges/5
        [ResponseType(typeof(IngredientMerge))]
        public async Task<IHttpActionResult> GetIngredientMerge(int id)
        {
            IngredientMerge ingredientMerge = await db.IngredientMerge.FindAsync(id);
            if (ingredientMerge == null)
            {
                return NotFound();
            }

            return Ok(ingredientMerge);
        }

        // PUT: api/IngredientMerges/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIngredientMerge(int id, IngredientMerge ingredientMerge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingredientMerge.IngredientMergeId)
            {
                return BadRequest();
            }

            db.Entry(ingredientMerge).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientMergeExists(id))
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

        // POST: api/IngredientMerges
        [ResponseType(typeof(IngredientMerge))]
        public async Task<IHttpActionResult> PostIngredientMerge(IngredientMerge ingredientMerge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IngredientMerge.Add(ingredientMerge);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ingredientMerge.IngredientMergeId }, ingredientMerge);
        }

        // DELETE: api/IngredientMerges/5
        [ResponseType(typeof(IngredientMerge))]
        public async Task<IHttpActionResult> DeleteIngredientMerge(int id)
        {
            IngredientMerge ingredientMerge = await db.IngredientMerge.FindAsync(id);
            if (ingredientMerge == null)
            {
                return NotFound();
            }

            db.IngredientMerge.Remove(ingredientMerge);
            await db.SaveChangesAsync();

            return Ok(ingredientMerge);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IngredientMergeExists(int id)
        {
            return db.IngredientMerge.Count(e => e.IngredientMergeId == id) > 0;
        }
    }
}