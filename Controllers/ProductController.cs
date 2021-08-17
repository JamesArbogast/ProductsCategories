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
        public ProductController(ProdCatContext context)
        {
          db = context;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            return View("AddProd");
        }

        // 1. handles GET request to DISPLAY the form used to create a new Post
        [HttpGet("/product/new")]
        public IActionResult New()
        {
            return View("AddProd");
        }

        // 2. handles POST request form submission to CREATE a new Post model instance
        [HttpPost("/products/create")]
        public IActionResult CreateProduct (Product newProduct)
        {
            // Every time a form is submitted, check the validations.
            if (ModelState.IsValid == false)
            {
                // Go back to the form so error messages are displayed.
                return RedirectToAction("New");
            }

            // The above return did not happen so ModelState IS valid.
            db.Products.Add(newProduct);
            // db doesn't update until we run save changes
            // after SaveChanges, our newPost object now has it's PostId updated from db auto generated id
            db.SaveChanges();
            return RedirectToAction("All");
        }

        [HttpGet("/products")]
        public IActionResult All()
        {
            List<Product> allProducts = db.Products.ToList();
            return View("AllProducts", allProducts);
        }

        [HttpGet("/products/{productId}")]
        public IActionResult Details(int productId)
        {
            Product product = db.Products.FirstOrDefault(c => c.ProductId == productId);
            if (product == null)
            {
                return RedirectToAction("All");
            }
            ViewBag.allCategories = db.Categories.ToList();
            return View("OneProd", product);
        }
        [HttpPost("/products/{productId}/sort")]
        public IActionResult Sort(int productId)
        {
            var sortedProduct = db.Products
              .Include(product => product.Categories)
                .ThenInclude(sort => sort.Product)
              .FirstOrDefault(product => product.ProductId == productId);
              
            SortedProduct sortProd = new SortedProduct()
            {
              ProductId = productId,
            };

              db.SortedProducts.Add(sortProd);
              db.SaveChanges();

              return View("OneProd", sortProd);
        }

        [HttpPost("/products/{productId}/delete")]
        public IActionResult Delete(int productId)
        {
            Product product = db.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return RedirectToAction("All");
            }

            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("All");
        }

        [HttpGet("/products/{productId}/edit")]
        public IActionResult Edit(int productId)
        {
            Product product = db.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
              return RedirectToAction("All");
            }

            return View("Edit", product);
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