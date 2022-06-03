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
    public class IBookController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<BookModel> bookCollection;

        //for grid controller

        public IBookController()
        {
            dbcontext = new MongoDBContext();
            bookCollection = dbcontext.database.GetCollection<BookModel>("book"); 
        }


        // GET: Book
        public ActionResult Index(string searchString)
        {
            try
            
            {
                List<BookModel> books = bookCollection.AsQueryable<BookModel>().ToList();

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


    }
}
