using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain;
using BackendCook.Models;
using BackendCook.Classes;

namespace BackendCook.Controllers
{
    public class RecipesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        #region Recipes
        // GET: Recipes
        public async Task<ActionResult> Index()
        {
            var recipes = db.Recipes.Include(r => r.Chef);
            return View(await recipes.ToListAsync());
        }

        // GET: Recipes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = await db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            ViewBag.ChefId = new SelectList(db.Chefs, "ChefId", "FirstName");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RecipeView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Photos";

                if (view.LogoFile != null)
                {
                    pic = FileUpload.UploadPhoto(view.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                var recipe = ToRecipe(view);
                recipe.Image = pic;
                db.Recipes.Add(recipe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ChefId = new SelectList(db.Chefs, "ChefId", "FirstName", view.ChefId);
            return View(view);
        }

        private Recipe ToRecipe(RecipeView view)
        {
            return new Recipe
            {
                Name = view.Name,
                Direction = view.Direction,
                ChefId = view.ChefId,
                Image = view.Image,
                Rating = view.Rating,
                RecipeId = view.RecipeId
            };
        }

        // GET: Recipes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = await db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChefId = new SelectList(db.Chefs, "ChefId", "FirstName", recipe.ChefId);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RecipeId,Name,Direction,Rating,ChefId,CuisineId,IngredientId")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ChefId = new SelectList(db.Chefs, "ChefId", "FirstName", recipe.ChefId);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = await db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Recipe recipe = await db.Recipes.FindAsync(id);
            db.Recipes.Remove(recipe);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region  IngredientMergesController

        // GET: IngredientMerges
        public async Task<ActionResult> IndexIngredientMerges()
        {
            var ingredientMerges = db.IngredientMerge.Include(i => i.Ingredient);
            return View(await ingredientMerges.ToListAsync());
        }

        // GET: IngredientMerges/Details/5
        public async Task<ActionResult> DetailsIngredientMerges(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientMerge ingredientMerge = await db.IngredientMerge.FindAsync(id);
            if (ingredientMerge == null)
            {
                return HttpNotFound();
            }
            return View(ingredientMerge);
        }

        // GET: IngredientMerges/Create
        public async Task<ActionResult> CreateIngredientMerges(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recipe = await db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            var view = new IngredientMerge { RecipeId = recipe.RecipeId };
            return View(view);
        }

        // POST: IngredientMerges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateIngredientMerges(IngredientMerge ingredientMerge)
        {
            if (ModelState.IsValid)
            {
                db.IngredientMerge.Add(ingredientMerge);
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", ingredientMerge.RecipeId));
            }
            return View(ingredientMerge);
        }

        // GET: IngredientMerges/Edit/5
        public async Task<ActionResult> EditIngredientMerges(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientMerge ingredientMerge = await db.IngredientMerge.FindAsync(id);
            if (ingredientMerge == null)
            {
                return HttpNotFound();
            }
            ViewBag.IngredientId = new SelectList(db.Ingredientes, "IngredientId", "Name", ingredientMerge.IngredientId);
            return View(ingredientMerge);
        }

        // POST: IngredientMerges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditIngredientMerges([Bind(Include = "IngredientMergeId,Name,IngredientId")] IngredientMerge ingredientMerge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredientMerge).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IngredientId = new SelectList(db.Ingredientes, "IngredientId", "Name", ingredientMerge.IngredientId);
            return View(ingredientMerge);
        }

        // GET: IngredientMerges/Delete/5
        public async Task<ActionResult> DeleteIngredientMerges(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientMerge ingredientMerge = await db.IngredientMerge.FindAsync(id);
            if (ingredientMerge == null)
            {
                return HttpNotFound();
            }
            return View(ingredientMerge);
        }

        //// POST: IngredientMerges/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    IngredientMerge ingredientMerge = await db.IngredientMerges.FindAsync(id);
        //    db.IngredientMerges.Remove(ingredientMerge);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #endregion

        #region CuisineMergesController 
        // GET: CuisineMerges
        public async Task<ActionResult> IndexCuisineMerges()
        {
            var cuisineMerges = db.CuisineMerges.Include(c => c.Cuisine);
            return View(await cuisineMerges.ToListAsync());
        }

        // GET: CuisineMerges/Details/5
        public async Task<ActionResult> DetailsCuisineMerges(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuisineMerge cuisineMerge = await db.CuisineMerges.FindAsync(id);
            if (cuisineMerge == null)
            {
                return HttpNotFound();
            }
            return View(cuisineMerge);
        }

        // GET: CuisineMerges/Create
        public async Task<ActionResult> CreateCuisineMerges(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recipe = await db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            var view = new CuisineMerge { RecipeId = recipe.RecipeId };
            return View(view);
        }

        // POST: CuisineMerges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCuisineMerges(CuisineMerge cuisineMerge)
        {
            if (ModelState.IsValid)
            {
                db.CuisineMerges.Add(cuisineMerge);
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", cuisineMerge.RecipeId));
            }

            return View(cuisineMerge);
        }

        // GET: CuisineMerges/Edit/5
        public async Task<ActionResult> EditCuisineMerges(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuisineMerge cuisineMerge = await db.CuisineMerges.FindAsync(id);
            if (cuisineMerge == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuisineId = new SelectList(db.Cuisines, "CuisineId", "Name", cuisineMerge.CuisineId);
            return View(cuisineMerge);
        }

        // POST: CuisineMerges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCuisineMerges([Bind(Include = "CuisineMergeId,Name,CuisineId")] CuisineMerge cuisineMerge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuisineMerge).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CuisineId = new SelectList(db.Cuisines, "CuisineId", "Name", cuisineMerge.CuisineId);
            return View(cuisineMerge);
        }

        // GET: CuisineMerges/Delete/5
        public async Task<ActionResult> DeleteCuisineMerges(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuisineMerge cuisineMerge = await db.CuisineMerges.FindAsync(id);
            if (cuisineMerge == null)
            {
                return HttpNotFound();
            }
            return View(cuisineMerge);
        }

        //// POST: CuisineMerges/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    CuisineMerge cuisineMerge = await db.CuisineMerges.FindAsync(id);
        //    db.CuisineMerges.Remove(cuisineMerge);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #endregion
    }
}
