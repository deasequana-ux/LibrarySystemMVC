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
    public class AccountController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<UsersModel> userCollection;

        public AccountController()
        {
            dbcontext = new MongoDBContext();
            userCollection = dbcontext.database.GetCollection<UsersModel>("user"); //we are getting collection //product is collection name
        }


  
        public ActionResult Index()
        {
            List<UsersModel> users = userCollection.AsQueryable<UsersModel>().ToList();
            return View(users);
        }

        public ActionResult Details(string id)
        {

            var userId = new ObjectId(id);
            var user = userCollection.AsQueryable<UsersModel>().SingleOrDefault(x => x.Id == userId);    
            return View(user);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(UsersModel user) //register
        {
            try
            {
                userCollection.InsertOne(user);      
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(string id)
        {

            var userId = new ObjectId(id);
            var user = userCollection.AsQueryable<UsersModel>().SingleOrDefault(x => x.Id == userId);
            return View(user);
           
        }

        [HttpPost]
        public ActionResult Edit(string id, UsersModel user)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<UsersModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<UsersModel>.Update
                    .Set("UserName", user.UserName)
                    .Set("UserSurname", user.UserEmail)
                    .Set("UserPassword", user.UserPassword);

                var result = userCollection.UpdateMany(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            var userId = new ObjectId(id);
            var user = userCollection.AsQueryable<UsersModel>().SingleOrDefault(x => x.Id == userId);
            return View(user);

        }

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
