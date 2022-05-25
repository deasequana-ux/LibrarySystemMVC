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
    public class UsersController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<UsersModel> userCollection;

        //for grid controller

        public UsersController()
        {
            dbcontext = new MongoDBContext();
            userCollection = dbcontext.database.GetCollection<UsersModel>("user"); //we are getting collection //product is collection name
        }


        // GET: Product
        public ActionResult Index()
        {

            // this is something(all list from prodect model) we returning back
            List<UsersModel> users = userCollection.AsQueryable<UsersModel>().ToList();
            return View(users);
        }

        // GET: Product/Details/5
        public ActionResult Details(string id)
        {

            var userId = new ObjectId(id);
            var user = userCollection.AsQueryable<UsersModel>().SingleOrDefault(x => x.Id == userId);    
            return View(user);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(UsersModel user)
        {
            try
            {
                // TODO: Add insert logic here

                userCollection.InsertOne(user);      //we are inserting
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

            var userId = new ObjectId(id);
            var user = userCollection.AsQueryable<UsersModel>().SingleOrDefault(x => x.Id == userId);
            return View(user);
           
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, UsersModel user)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<UsersModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<UsersModel>.Update
                    .Set("UserName", user.UserName)
                    .Set("UserSurname", user.UserSurname)
                    .Set("UserPhoneNumber", user.UserPhoneNumber)
                    .Set("UserAddress", user.UserAddress)
                    .Set("User_Name", user.User_Name)
                    .Set("UserPassword", user.UserPassword);

                var result = userCollection.UpdateMany(filter, update);
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
            var userId = new ObjectId(id);
            var user = userCollection.AsQueryable<UsersModel>().SingleOrDefault(x => x.Id == userId);
            return View(user);

        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                userCollection.DeleteOne(Builders<UsersModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
