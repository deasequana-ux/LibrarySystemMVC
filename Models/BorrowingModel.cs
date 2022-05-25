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

        [BsonElement("DueDate")] //bitiş tarihi
        public string DueDate { get; set; }

        [BsonElement("ReturnDate")] //teslim ettiği tarih
        public string ReturnDate { get; set; }
        public BookModel BookInfo { get; set; }
        public PublisherModel PublisherInfo { get; set; } //publisher does the financial thinking
        public UsersModel UsersInfo { get; set; }



    }
}