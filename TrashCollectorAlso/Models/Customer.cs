using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Globalization;

namespace TrashCollectorAlso.Models
{
    public class Customer //tlc
    {
        [Key]
        public int Id { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        [Display(Name = "Weekly Pickup Day")]
        public int weeklyPickupDay { get; set; }
        public double balance { get; set; }
        public double monthlyCharge { get; set; }
        //public double extraCharge { get; set; }
        public bool serviceIsSuspended { get; set; }
        public DateTime serviceStartDate { get; set; }
        public DateTime serviceEndDate { get; set; }
        public bool pickupConfirmed { get; set; }
        public double pickupCharge { get; set; }
        public bool extraPickupRequested { get; set; }
        public DateTime extraPickupDate { get; set; }
        public bool extraPickupConfirmed { get; set; }
        public float extraPickupCharge { get; set; }

        //tlc
        [ForeignKey("ApplicationUser")]//fk attr
        public string ApplicationId { get; set; } //fk's spot at the table
        public ApplicationUser ApplicationUser { get; set; }//the class the fk attr is referencing

        public IEnumerable<SelectListItem> Days //tlc
        {
            get
            {
                return DateTimeFormatInfo
                       .InvariantInfo
                       .DayNames
                       .Select((dayName, index) => new SelectListItem
                       {
                           //Value = (index + 1).ToString(),
                           Value = index.ToString(),
                           Text = dayName
                       });
            }
        }

    }
}