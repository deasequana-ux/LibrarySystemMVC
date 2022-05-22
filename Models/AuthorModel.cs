using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LibrarySystemMVC.Models
{
    public class AuthorModel
    {
        [BsonId]     
        public ObjectId Id { get; set; }

        [BsonElement("AuthorName")]
        public string AuthorName { get; set; }

        [BsonElement("AuthorSurname")]
        public string AuthorSurname { get; set; }

        [BsonElement("AuthorEmail")]
        public string AuthorEmail { get; set; }


    }
}