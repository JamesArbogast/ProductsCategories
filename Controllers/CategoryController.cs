using System;
using System.Collections.Generic;
using System.Linq;
using ProductsCategories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductsCategories.Controllers
{
    public class CategoryController : Controller
    {
        private ProdCatContext db;
        public CategoryController(ProdCatContext context)
        {
            db = context;
        }


        // public IActionResult Index()
        // {
        //     return View("");
        // }

        // 1. handles GET request to DISPLAY the form used to create a new Post
        [HttpGet("/category/new")]
        public IActionResult New()
        {
            return View("AddCategories");
        }

        // 2. handles POST request form submission to CREATE a new Post model instance
        [HttpPost("/categories/create")]
        public IActionResult CreateCategory (Category newCategory)
        {
            // Every time a form is submitted, check the validations.
            if (ModelState.IsValid == false)
            {
                // Go back to the form so error messages are displayed.
                return RedirectToAction("New");
            }

            // The above return did not happen so ModelState IS valid.
            db.Categories.Add(newCategory);
            // db doesn't update until we run save changes
            // after SaveChanges, our newPost object now has it's PostId updated from db auto generated id
            db.SaveChanges();
            return RedirectToAction("All");
        }

        [HttpGet("/categories")]
        public IActionResult All()
        {
            List<Category> allCategories = db.Categories.ToList();
            return View("AllCategories", allCategories);
        }

        [HttpPost("/products/{categoryId}/sort")]
        public IActionResult Sort(int categoryId, SortedProduct sortedProd)
        {
            ViewBag.prods = db.Products.ToList();
            sortedProd.CategoryId = categoryId;
            db.SortedProducts.Add(sortedProd);
            db.SaveChanges();

            return RedirectToAction("Details", new {categoryId = categoryId});
        }

        [HttpGet("/categories/{categoryId}")]
        public IActionResult Details(int categoryId)
        {
            Category category = db.Categories
                // .Include is always giving you the entity from the table
                // being queried, for you to include a property from that entity.
                .Include(category => category.SortedProducts)
                // .ThenInclude is always giving you the datatype of what was
                // just previously included so you can include a property on
                // the entity that was just included.
                .ThenInclude(sortProd => sortProd.Product)
                .FirstOrDefault(category => category.CategoryId == categoryId);
            if (category == null)
            {
                return RedirectToAction("All");
            }
            ViewBag.allProducts = db.Products.ToList();
            return View("OneCategory", category);
        }

        [HttpPost("/categories/{categoryId}/delete")]
        public IActionResult Delete(int categoryId)
        {
            Category category = db.Categories.FirstOrDefault(p => p.CategoryId == categoryId);

            if (category == null)
            {
                return RedirectToAction("All");
            }

            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("All");
        }

        [HttpGet("/categories/{categoryId}/edit")]
        public IActionResult Edit(int categoryId)
        {
            Category category = db.Categories.FirstOrDefault(p => p.CategoryId == categoryId);

            if (category == null)
            {
                return RedirectToAction("All");
            }

            return View("Edit", category);
        }

        // [HttpPost("/posts/{postId}/update")]
        // public IActionResult Update(int postId, Post editedPost)
        // {
        //     if (ModelState.IsValid == false)
        //     {
        //         editedPost.PostId = postId;
        //         // Send back to the page with the current form edited data to
        //         // display errors.
        //         return View("Edit", editedPost);
        //     }

        //     Post dbPost = db.Posts.FirstOrDefault(p => p.PostId == postId);

        //     if (dbPost == null)
        //     {
        //         return RedirectToAction("All");
        //     }

        //     dbPost.Topic = editedPost.Topic;
        //     dbPost.Body = editedPost.Body;
        //     dbPost.ImgUrl = editedPost.ImgUrl;
        //     dbPost.UpdatedAt = DateTime.Now;

        //     db.Posts.Update(dbPost);
        //     db.SaveChanges();

        //     /* 
        //     When redirecting to action that has params, you need to pass in a
        //     dict with keys that match param names and the value of the keys are
        //     the values for the params.
        //     */
        //     return RedirectToAction("Details", new { postId = postId });
        // }
    }
}