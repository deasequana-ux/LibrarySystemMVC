using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LibrarySystemMVC.Models
{
    public class PublisherModel
    {
        [BsonId]     
        public ObjectId Id { get; set; }

        [BsonElement("PublisherName")]
        public string PublisherName { get; set; }

        [BsonElement("PublisherPhoneNumber")]
        public string PublisherPhoneNumber { get; set; }

        [BsonElement("PublisherAddress")]
        public string PublisherAddress { get; set; }

        [BsonElement("PublisherWebsite")]
        public string PublisherWebsite { get; set; }


    }
}