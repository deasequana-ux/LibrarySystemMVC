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
using System.Web.Security;

namespace LibrarySystemMVC.Controllers
{
    public class UsersController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<UsersModel> userCollection;



        public UsersController()
        {
            dbcontext = new MongoDBContext();
            userCollection = dbcontext.database.GetCollection<UsersModel>("user");
        }


        public ActionResult Index()
        {

            List<UsersModel> users = userCollection.AsQueryable<UsersModel>().ToList();
            return View(users);
        }

        public ActionResult Details(string id)
        {

            var userId = new ObjectId(id);
            var user = userCollection.AsQueryable<UsersModel>().SingleOrDefault(x => x.UserId == userId);
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UsersModel user)
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
            var user = userCollection.AsQueryable<UsersModel>().SingleOrDefault(x => x.UserId == userId);
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
                    .Set("UserEmail", user.UserEmail)
                    .Set("UserPassword", user.UserPassword)
                    .Set("UserRole", user.UserRole);

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
            var user = userCollection.AsQueryable<UsersModel>().SingleOrDefault(x => x.UserId == userId);
            return View(user);

        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                userCollection.DeleteOne(Builders<UsersModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return Redirect("/Users/Admin");

        }

        [HttpPost]
        public ActionResult Login(UsersModel users)
        {
            var response = userCollection.AsQueryable<UsersModel>().FirstOrDefault(x => x.UserEmail == users.UserEmail && x.UserPassword == users.UserPassword && x.UserRole == users.UserRole);
            if (response != null)
            {
                FormsAuthentication.SetAuthCookie(users.UserEmail, false);
                if (users.UserRole == "Admin")
                {
                    return RedirectToAction("Admin", "Users");
                }
                else
                {
                    return RedirectToAction("Index", "Book");
                }

            }
            else
            {
                ViewBag.msg = "Wrong credentials!";
                return View();
            }

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UsersModel user)
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


        public ActionResult Admin()
        {
            return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();  //Kullanıcıya çıkış yaptırdık 
            return RedirectToAction("Login");
        }
    }
}
