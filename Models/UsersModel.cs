using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LibrarySystemMVC.Models
{
    public class UsersModel
    {
        [BsonId]     
        public ObjectId Id { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("UserEmail")]
        public string UserEmail { get; set; }

        [BsonElement("UserPassword")]
        public string UserPassword { get; set; }

    }
}