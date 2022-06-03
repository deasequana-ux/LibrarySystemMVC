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
    public class BorrowingController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<BorrowingModel> borrowingCollection;
        private IMongoCollection<BookModel> bookCollection;

        //for grid controller

        public BorrowingController()
        {
            dbcontext = new MongoDBContext();
            borrowingCollection = dbcontext.database.GetCollection<BorrowingModel>("borrowing"); //we are getting collection //product is collection name
            bookCollection = dbcontext.database.GetCollection<BookModel>("book");
        }


        // GET: Product
        public ActionResult Index()
        {

            // this is something(all list from prodect model) we returning back
            List<BorrowingModel> borrowings = borrowingCollection.AsQueryable<BorrowingModel>().ToList();
            var userId = Session["UserId"] as string;

            var borrowList = borrowings.Where(b => b.UserId == userId).ToList();

            return View(borrowList);
        }

        // GET: Product/Details/5
        public ActionResult Details(string id)
        {

            var borrowingId = new ObjectId(id);
            var borrowing = borrowingCollection.AsQueryable<BorrowingModel>().SingleOrDefault(x => x.Id == borrowingId);
            return View(borrowing);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(BorrowingModel borrowing)
        {
            try
            {
                // TODO: Add insert logic here

                borrowingCollection.InsertOne(borrowing);      //we are inserting
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult BorrowBook(string BookId, string BookName)
        {
            var userId = Session["UserId"] as string;

            var borrowModel = new BorrowingModel()
            {
                BookId = BookId,
                BorrowDate = DateTime.Now.ToString("dd:MMM:yyyy"),
                BookName = BookName,
                UserId = userId,
            };

            // add to db
            borrowingCollection.InsertOne(borrowModel);


            List<BookModel> books = bookCollection.AsQueryable<BookModel>().ToList();
            var bookCount = books.FirstOrDefault(b => b.BookId.ToString() == BookId).NumberOfBook - 1;

            var bookFilter = Builders<BookModel>.Filter.Eq("_id", ObjectId.Parse(BookId));
            var update = Builders<BookModel>.Update
                .Set("NumberOfBook", bookCount);




            var result = bookCollection.UpdateMany(bookFilter, update);

            return RedirectToAction("Index", "Borrowing");

        }

        public ActionResult ReturnBook(string BookId)
        {
            var userId = Session["UserId"] as string;
            List<BorrowingModel> borrowings = borrowingCollection.AsQueryable<BorrowingModel>().ToList();

            var relatedBorrowing = borrowings.FirstOrDefault(b => b.BookId == BookId && b.UserId == userId);

            var filter = Builders<BorrowingModel>.Filter.Eq("_id", ObjectId.Parse(relatedBorrowing.Id.ToString()));
            var borrowModel = Builders<BorrowingModel>.Update
                    .Set("ReturnDate", DateTime.Now.ToString("dd:MMM:yyyy"))
                    .Set("BookId", relatedBorrowing.BookId)
                    .Set("User", relatedBorrowing.UserId);


            var x = borrowingCollection.UpdateMany(filter, borrowModel);

            List<BookModel> books = bookCollection.AsQueryable<BookModel>().ToList();
            var bookCount = books.FirstOrDefault(b => b.BookId.ToString() == BookId).NumberOfBook + 1;

            var bookFilter = Builders<BookModel>.Filter.Eq("_id", ObjectId.Parse(BookId));
            var update = Builders<BookModel>.Update
                .Set("NumberOfBook", bookCount);

            var result = bookCollection.UpdateMany(bookFilter, update);

            return RedirectToAction("Index", "Borrowing");

        }


        // GET: Product/Edit/5
        public ActionResult Edit(string id)
        {

            var borrowingId = new ObjectId(id);
            var borrowing = borrowingCollection.AsQueryable<BorrowingModel>().SingleOrDefault(x => x.Id == borrowingId);
            return View(borrowing);

        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, BorrowingModel borrowing)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<BorrowingModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<BorrowingModel>.Update
                    .Set("BorrowDate", borrowing.BorrowDate)
                    .Set("ReturnDate", borrowing.ReturnDate)
                    .Set("BookId", borrowing.BookId)
                    .Set("User", borrowing.UserId);



                var result = borrowingCollection.UpdateMany(filter, update);
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
            var borrowingId = new ObjectId(id);
            var borrowing = borrowingCollection.AsQueryable<BorrowingModel>().SingleOrDefault(x => x.Id == borrowingId);
            return View(borrowing);

        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                borrowingCollection.DeleteOne(Builders<BorrowingModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
