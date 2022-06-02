using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LibrarySystemMVC.Models
{
    public class LoginModel
    {
        [BsonId]     
        public ObjectId Id { get; set; }

        [BsonElement("LoginUsername")]
        public string LoginUsername { get; set; }

        [BsonElement("LoginPassword")]
        public string LoginPassword { get; set; }

   

    }
}