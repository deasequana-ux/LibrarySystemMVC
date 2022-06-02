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
        public ObjectId UserId { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("UserSurname")]
        public string UserSurname { get; set; }

        [BsonElement("UserPhoneNumber")]
        public string UserPhoneNumber { get; set; }

        [BsonElement("UserAddress")]
        public string UserAddress { get; set; }

        [BsonElement("User_Name")]
        public string User_Name { get; set; }

        [BsonElement("UserEmail")]
        public string UserEmail { get; set; }

        [BsonElement("UserPassword")]
        public string UserPassword { get; set; }

    }
}