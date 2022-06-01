//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using MongoDB.Bson; //all line added extra
//using System.Configuration;
//using MongoDB.Driver.Core;
//using LibrarySystemMVC.App_Start;     //for handling  error and including this page as start coz controller is everything manage
//using MongoDB.Driver;
//using LibrarySystemMVC.Models;

//namespace LibrarySystemMVC.Controllers
//{
//    public class BorrowingController : Controller
//    {
//        private MongoDBContext dbcontext;
//        private IMongoCollection<BorrowingModel> borrowingCollection;

//        //for grid controller

//        public BorrowingController()
//        {
//            dbcontext = new MongoDBContext();
//            borrowingCollection = dbcontext.database.GetCollection<BorrowingModel>("borrowing"); //we are getting collection //product is collection name
//        }


//        // GET: Product
//        public ActionResult Index()
//        {

//            // this is something(all list from prodect model) we returning back
//            List<BorrowingModel> borrowings = borrowingCollection.AsQueryable<BorrowingModel>().ToList();
//            return View(borrowings);
//        }

//        // GET: Product/Details/5
//        public ActionResult Details(string id)
//        {

//            var borrowingId = new ObjectId(id);
//            var borrowing = borrowingCollection.AsQueryable<BorrowingModel>().SingleOrDefault(x => x.Id == borrowingId);    
//            return View(borrowing);
//        }

//        // GET: Product/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Product/Create
//        [HttpPost]
//        public ActionResult Create(BorrowingModel borrowing)
//        {
//            try
//            {
//                // TODO: Add insert logic here

//                borrowingCollection.InsertOne(borrowing);      //we are inserting
//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: Product/Edit/5
//        public ActionResult Edit(string id)
//        {

//            var borrowingId = new ObjectId(id);
//            var borrowing = borrowingCollection.AsQueryable<BorrowingModel>().SingleOrDefault(x => x.Id == borrowingId);
//            return View(borrowing);
           
//        }

//        // POST: Product/Edit/5
//        [HttpPost]
//        public ActionResult Edit(string id, BorrowingModel borrowing)
//        {
//            try
//            {
//                // TODO: Add update logic here
//                var filter = Builders<BorrowingModel>.Filter.Eq("_id", ObjectId.Parse(id));
//                var update = Builders<BorrowingModel>.Update
//                    .Set("BorrowDate", borrowing.BorrowDate)
//                    .Set("DueDate", borrowing.DueDate)
//                    .Set("ReturnDate", borrowing.ReturnDate)
//                    .Set("AuthorName", borrowing.BookInfo.BookName)
//                    .Set("PublisherName", borrowing.PublisherInfo.PublisherName)
//                    .Set("PublisherPhoneNumber", borrowing.PublisherInfo.PublisherPhoneNumber)
//                    .Set("PublisherAddress", borrowing.PublisherInfo.PublisherAddress)
//                    .Set("PublisherWebsite", borrowing.PublisherInfo.PublisherWebsite)
//                    .Set("PublishYear", borrowing.PublisherInfo.PublishYear);

//                var result = borrowingCollection.UpdateMany(filter, update);
//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: Product/Delete/5
//        public ActionResult Delete(string id)
//        {
//            var borrowingId = new ObjectId(id);
//            var borrowing = borrowingCollection.AsQueryable<BorrowingModel>().SingleOrDefault(x => x.Id == borrowingId);
//            return View(borrowing);

//        }

//        // POST: Product/Delete/5
//        [HttpPost]
//        public ActionResult Delete(string id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add delete logic here
//                borrowingCollection.DeleteOne(Builders<BorrowingModel>.Filter.Eq("_id", ObjectId.Parse(id)));
//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}
