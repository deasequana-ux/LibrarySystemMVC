using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson; //all line added extra
using System.Configuration;
using MongoDB.Driver.Core;
using LibrarySystemMVC.App_Start;     //for handling  error and including this page as start coz controller is everything manage
using MongoDB.Driver;
using LibrarySystemMVC.Models;

namespace LibrarySystemMVC.Controllers
{
    public class AuthorController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<AuthorModel> authorCollection;

        //for grid controller

        public AuthorController()
        {
            dbcontext = new MongoDBContext();
            authorCollection = dbcontext.database.GetCollection<AuthorModel>("author"); //we are getting collection //product is collection name
        }


        // GET: Product
        public ActionResult Index()
        {

            // this is something(all list from prodect model) we returning back
            List<AuthorModel> authors = authorCollection.AsQueryable<AuthorModel>().ToList();
            return View(authors);
        }

        // GET: Product/Details/5
        public ActionResult Details(string id)
        {

            var authorId = new ObjectId(id);
            var author = authorCollection.AsQueryable<AuthorModel>().SingleOrDefault(x => x.Id == authorId);    
            return View(author);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(AuthorModel author)
        {
            try
            {
                // TODO: Add insert logic here

                authorCollection.InsertOne(author);      //we are inserting
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(string id)
        {

            var authorId = new ObjectId(id);
            var author = authorCollection.AsQueryable<AuthorModel>().SingleOrDefault(x => x.Id == authorId);
            return View(author);
           
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, AuthorModel author)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<AuthorModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<AuthorModel>.Update
                    .Set("AuthorName", author.AuthorName)
                    .Set("AuthorSurname", author.AuthorSurname)
                    .Set("AuthorEmail", author.AuthorEmail);

                var result = authorCollection.UpdateMany(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(string id)
        {
            var authorId = new ObjectId(id);
            var author = authorCollection.AsQueryable<AuthorModel>().SingleOrDefault(x => x.Id == authorId);
            return View(author);

        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                authorCollection.DeleteOne(Builders<AuthorModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
