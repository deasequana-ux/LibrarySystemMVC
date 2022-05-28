using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LibrarySystemMVC.Models
{
    public class BookModel
    {
        [BsonId]
        public ObjectId BookId { get; set; }

        [BsonElement("ISBNNO")]
        public int ISBNNO { get; set; }

        [BsonElement("BookName")]
        public string BookName { get; set; }

        [BsonElement("PageNumber")]
        public int PageNumber { get; set; }

        [BsonElement("Language")]
        public string Language { get; set; }

        [BsonElement("Category")]
        public string Category { get; set; }

        [BsonElement("NumberOfBook")]
        public int NumberOfBook { get; set; }
        
        [BsonElement("Edition")]
        public int Edition { get; set; }

        [BsonElement("Editor")] //An editor does the editing
        public string Editor { get; set; }
        public AuthorModel AuthorInfo { get; set; }
        public PublisherModel PublisherInfo  { get; set; } //publisher does the financial thinking

    }
}