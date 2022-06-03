using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Please enter your email")]
        [BsonElement("UserEmail")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [BsonElement("UserPassword")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Please select your role")]
        [BsonElement("UserRole")]
        public string UserRole { get; set; }

        //public IList<BookModel> Books { get; }  //to-many relationship 

    }
}