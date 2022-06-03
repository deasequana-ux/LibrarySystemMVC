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
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace LibrarySystemMVC.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    public class BookController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<BookModel> bookCollection;
        private IMongoCollection<BorrowingModel> borrowCollection;

        //for grid controller

        public BookController()
        {
            dbcontext = new MongoDBContext();
            bookCollection = dbcontext.database.GetCollection<BookModel>("book");
            borrowCollection = dbcontext.database.GetCollection<BorrowingModel>("borrowing"); 
        }


        // GET: Book
        public ActionResult Index(string searchString)
        {
            try
            
            {
                List<BookModel> books = bookCollection.AsQueryable<BookModel>().ToList();

                List<BorrowingModel> borrows = borrowCollection.AsQueryable<BorrowingModel>().ToList();

                if (!string.IsNullOrEmpty(searchString))
                {
                   var result = books.Where(x => x.BookName.Contains(searchString)).ToList(); //search
                   return View("Index", result);
                }
                return View("Index", books);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        // GET: Book/Details/5
        public ActionResult Details(string id)
        {

            var bookId = new ObjectId(id);
            var book = bookCollection.AsQueryable<BookModel>().SingleOrDefault(x => x.BookId == bookId);    
            return View(book);
        }

        // GET: Book/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(BookModel book)
        {
            try
            {
                // TODO: Add insert logic here
                bookCollection.InsertOne(book);
                return View("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(string id)
        {

            var bookId = new ObjectId(id);
            var book = bookCollection.AsQueryable<BookModel>().SingleOrDefault(x => x.BookId == bookId);
            return View(book);
           
        }

        [HttpPost]
        public ActionResult Edit(string id, BookModel book)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<BookModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<BookModel>.Update
                    .Set("_id", ObjectId.Parse(id))
                    .Set("ISBNNO", book.ISBNNO)
                    .Set("BookName", book.BookName)
                    .Set("PageNumber", book.PageNumber)
                    .Set("Language", book.Language)
                    .Set("Category", book.Category)
                    .Set("NumberOfBook", book.NumberOfBook)
                    .Set("Edition", book.Edition)
                    .Set("Editor", book.Editor)
                    .Set("Author", book.Author)
                    .Set("Publisher", book.Publisher)
                    .Set("PublishYear", book.PublishYear);

    


                var result = bookCollection.UpdateMany(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(string id)
        {
            var bookId = new ObjectId(id);
            var book = bookCollection.AsQueryable<BookModel>().SingleOrDefault(x => x.BookId == bookId);
            return View(book);

        }


        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                bookCollection.DeleteOne(Builders<BookModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
