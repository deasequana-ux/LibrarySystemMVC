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
    public class PublisherController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<PublisherModel> publisherCollection;

        //for grid controller

        public PublisherController()
        {
            dbcontext = new MongoDBContext();
            publisherCollection = dbcontext.database.GetCollection<PublisherModel>("publisher"); //we are getting collection //product is collection name
        }


        // GET: Product
        public ActionResult Index()
        {

            // this is something(all list from prodect model) we returning back
            List<PublisherModel> publishers = publisherCollection.AsQueryable<PublisherModel>().ToList();
            return View(publishers);
        }

        // GET: Product/Details/5
        public ActionResult Details(string id)
        {

            var publisherId = new ObjectId(id);
            var publisher = publisherCollection.AsQueryable<PublisherModel>().SingleOrDefault(x => x.Id == publisherId);    
            return View(publisher);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(PublisherModel publisher)
        {
            try
            {
                // TODO: Add insert logic here

                publisherCollection.InsertOne(publisher);      //we are inserting
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

            var publisherId = new ObjectId(id);
            var publisher = publisherCollection.AsQueryable<PublisherModel>().SingleOrDefault(x => x.Id == publisherId);
            return View(publisher);
           
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, PublisherModel publisher)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<PublisherModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<PublisherModel>.Update
                    .Set("PublisherName", publisher.PublisherName)
                    .Set("PublisherPhoneNumber", publisher.PublisherPhoneNumber)
                    .Set("PublisherAddress", publisher.PublisherAddress)
                    .Set("PublisherWebsite", publisher.PublisherWebsite)
                    .Set("PublishYear", publisher.PublishYear);


                var result = publisherCollection.UpdateMany(filter, update);
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
            var publisherId = new ObjectId(id);
            var publisher = publisherCollection.AsQueryable<PublisherModel>().SingleOrDefault(x => x.Id == publisherId);
            return View(publisher);

        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                publisherCollection.DeleteOne(Builders<PublisherModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
