using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public record Book
    {
        public Book()
        {
            this._id = ObjectId.GenerateNewId(DateTime.Now).ToString();
        }
        public String Name { get; set; }                        
        //Name of the book
        public String Author { get; set; }                      
        //Name of the Author
        [BsonId]
        public String _id { get; set; }                      
        //Unique ID
        public int Cost { get; set; }                           
        //Cost of the book
        public List<IssuerDetails> Issuers { get; set; }               
        //History of subscriptions
        public bool Available { get; set; }                     
        //If available or issued
        public double FinePerDay { get; set; }                      
        //Fine per day for late return
    }

    public record IssuerDetails
    {
        public String CustomerID { get; set; }                       
        //Subscriber Details
        public DateTime SubscriptionDate { get; set; }                          
        //Date of subscription
        public int SubscriptionDuration { get; set; }                           
        //Subscription Duration in days
        public DateTime DateOfReturn { get; set; }                              
        //Date of return of the book
        public double Fine => DateOfReturn.Subtract(SubscriptionDate).TotalDays - SubscriptionDuration;
        //Fine implied due to late return
    }
}
