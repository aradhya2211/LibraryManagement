using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public record Subscribers
    {
        [BsonId]
        public ObjectId ID { get; set; }
        //MongoDb ID
        [BsonId(IdGenerator = typeof(CounterIdGenerator))]
        public String CustomerID { get; set; }
        //Customer ID
        public String Name { get; set; }
        //Customer Name
        public int Age { get; set; }
        //Customer Age
        public String EmailId { get; set; }
        //Customer Email ID
        public DateTime DateOfJoining { get; set; }
        //Date of Joining
        public int MembershipPlan { get; set; }
        //Months of Plan Taken
        public double Fine { get; set; }
        //Fine for late return
        public double TotalFinePaid { get; set; }
        //Total fine paid till now
        public DateTime MembershipExpiry => DateOfJoining.AddMonths(MembershipPlan);
        //Membership Expiration Date
    }

    internal class CounterIdGenerator : IIdGenerator
    {
        private static int _counter = 100; 
        public object GenerateId(object container, object document) 
        {
            DateTime dateTime = new DateTime();
            return $"Subs/{dateTime.Year.ToString()}/{_counter++.ToString()}"; 
        }
        public bool IsEmpty(object id) 
        { 
            return id.Equals(default(String)); 
        }
    }
}
