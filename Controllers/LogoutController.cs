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
    public class LogoutController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<LoginModel> loginCollection;



        public LogoutController()
        {
            dbcontext = new MongoDBContext();
            loginCollection = dbcontext.database.GetCollection<LoginModel>("login"); //we are getting collection //product is collection name
        }


        public ActionResult Index()
        {

            // this is something(all list from prodect model) we returning back
            List<LoginModel> logins = loginCollection.AsQueryable<LoginModel>().ToList();
            return View(logins);
        }

        public ActionResult Details(string id)
        {

            var loginId = new ObjectId(id);
            var login = loginCollection.AsQueryable<LoginModel>().SingleOrDefault(x => x.Id == loginId);    
            return View(login);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(LoginModel login)
        {
            try
            {

                loginCollection.InsertOne(login);      
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(string id)
        {

            var loginId = new ObjectId(id);
            var login = loginCollection.AsQueryable<LoginModel>().SingleOrDefault(x => x.Id == loginId);
            return View(login);
           
        }

        [HttpPost]
        public ActionResult Edit(string id, LoginModel login)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<LoginModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<LoginModel>.Update
                    .Set("LoginUsername", login.LoginUsername)
                    .Set("LoginPassword", login.LoginPassword);

                var result = loginCollection.UpdateMany(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            var loginId = new ObjectId(id);
            var login = loginCollection.AsQueryable<LoginModel>().SingleOrDefault(x => x.Id == loginId);
            return View(login);

        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                loginCollection.DeleteOne(Builders<LoginModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
