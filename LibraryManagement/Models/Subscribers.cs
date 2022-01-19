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
        public Subscribers()
        {
            this.CustomerId = ObjectId.GenerateNewId(DateTime.Now).ToString();
        }
        [BsonId]
        public String CustomerId { get; set; }
        //MongoDb ID
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

}
