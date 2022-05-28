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
    public class BookController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<BookModel> bookCollection;

        //for grid controller

        public BookController()
        {
            dbcontext = new MongoDBContext();
            bookCollection = dbcontext.database.GetCollection<BookModel>("book"); //we are getting collection //product is collection name
        }


        // GET: Book
        public ActionResult Index()
        {
            try
            {
                // this is something(all list from prodect model) we returning back
                List<BookModel> books = bookCollection.AsQueryable<BookModel>().ToList();
                return View(books);
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
        public ActionResult Create()
        {
            List<BookModel> books = bookCollection.AsQueryable<BookModel>().ToList();
            return View(books);
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(BookModel book)
        {
            try
            {
                // TODO: Add insert logic here
                bookCollection.InsertOne(book);      //we are inserting
                return View("Index");
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

        // POST: Product/Edit/5
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
                    .Set("AuthorInfo", book.AuthorInfo)
                    .Set("PublisherInfo", book.PublisherInfo);
                    //.Set("AuthorName", book.AuthorInfo.AuthorName)
                    //.Set("AuthorSurname", book.AuthorInfo.AuthorSurname)
                    //.Set("AuthorEmail", book.AuthorInfo.AuthorEmail)
                    //.Set("PublisherName", book.PublisherInfo.PublisherName)
                    //.Set("PublisherPhoneNumber", book.PublisherInfo.PublisherPhoneNumber)
                    //.Set("PublisherAddress", book.PublisherInfo.PublisherAddress)
                    //.Set("PublisherWebsite", book.PublisherInfo.PublisherWebsite)
                    //.Set("PublishYear", book.PublisherInfo.PublishYear);


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

        // choose author
        public ActionResult ChooseAuthor(AuthorModel author)
        {
            var book = bookCollection.AsQueryable<BookModel>().SingleOrDefault(x => x.AuthorInfo.Id == author.Id);
            return View(book);

        }

        // choose publisher
        public ActionResult ChoosePublisher(PublisherModel publisher)
        {
            var book = bookCollection.AsQueryable<BookModel>().SingleOrDefault(x => x.AuthorInfo.Id == publisher.Id);
            return View(book);

        }


    }
}
