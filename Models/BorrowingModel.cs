using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LibrarySystemMVC.Models
{
    public class BorrowingModel
    {
        [BsonId]     
        public ObjectId Id { get; set; }

        [BsonElement("BorrowDate")] //Ödünç alınan tarih
        public string BorrowDate { get; set; }

        [BsonElement("ReturnDate")] //teslim ettiği tarih
        public string ReturnDate { get; set; }

        [BsonElement("Book")]
        public string BookId { get; set; }

        [BsonElement("BookName")]
        public string BookName { get; set; }

        [BsonElement("User")]
        public string UserId { get; set; }

    }
}