using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public record Subscribers
    {
        public String ID { get; set; }                                       //Customer ID
        public String Name { get; set; }                                     //Customer Name
        public int Age { get; set; }                                         //Customer Age
        public String EmailId { get; set; }                                  //Customer Email ID
        public DateTime DateOfJoining { get; set; }                          //Date of Joining
        public int MembershipPlan { get; set; }                              //Months of Plan Taken
        public int Fine { get; set; }                                        //Fine for late return
        public DateTime MembershipExpiry => DateOfJoining.AddMonths(MembershipPlan);      //Membership Expiration Date
    }
}
