using System;
using System.Collections.Generic;
using System.Linq;
using ProductsCategories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductsCategories.Controllers
{
    public class ProductController : Controller
    {
        private ProdCatContext db;
        public ChefController(ChefDishContext context)
        {
            db = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        // 1. handles GET request to DISPLAY the form used to create a new Post
        [HttpGet("/chef/new")]
        public IActionResult New()
        {
            return View("AddChef");
        }

        // 2. handles POST request form submission to CREATE a new Post model instance
        [HttpPost("/chefs/create")]
        public IActionResult CreateChef (Chef newChef)
        {
            // Every time a form is submitted, check the validations.
            if (ModelState.IsValid == false)
            {
                // Go back to the form so error messages are displayed.
                return RedirectToAction("New");
            }

            // The above return did not happen so ModelState IS valid.
            db.Chefs.Add(newChef);
            // db doesn't update until we run save changes
            // after SaveChanges, our newPost object now has it's PostId updated from db auto generated id
            db.SaveChanges();
            return RedirectToAction("All");
        }

        [HttpGet("/chefs")]
        public IActionResult All()
        {
            List<Chef> allChefs = db.Chefs
            .Include(chefs => chefs.CreatedDishes)
            .ToList();
            return View("Chefs", allChefs);
        }

        [HttpGet("/chefs/{chefId}")]
        public IActionResult Details(int chefId)
        {
            Chef chef = db.Chefs.FirstOrDefault(c => c.ChefId == chefId);

            if (chef == null)
            {
                return RedirectToAction("All");
            }

            return View("Details", chef);
        }

        [HttpPost("/chefs/{chefId}/delete")]
        public IActionResult Delete(int chefId)
        {
            Chef chef = db.Chefs.FirstOrDefault(p => p.ChefId == chefId);

            if (chef == null)
            {
                return RedirectToAction("All");
            }

            db.Chefs.Remove(chef);
            db.SaveChanges();
            return RedirectToAction("All");
        }

        [HttpGet("/chefs/{chefId}/edit")]
        public IActionResult Edit(int chefId)
        {
            Chef chef = db.Chefs.FirstOrDefault(p => p.ChefId == chefId);

            if (chef == null)
            {
                return RedirectToAction("All");
            }

            return View("Edit", chef);
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