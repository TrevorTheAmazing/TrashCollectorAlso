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
        [Display(Name = "First")]
        public string firstName { get; set; }
        [Display(Name = "Last")]
        public string lastName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        [Display(Name = "Zip")]
        public string zip { get; set; }
        [Display(Name = "Weekly Pickup Day")]
        public int weeklyPickupDay { get; set; }
        [Display(Name = "Balance")]
        public double balance { get; set; }
        //public double monthlyCharge { get; set; }
        //public double extraCharge { get; set; }
        [Display(Name = "Suspended")]
        public bool serviceIsSuspended { get; set; }
        [Display(Name = "Service Start")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime serviceStartDate { get; set; }
        [Display(Name = "Service End")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime serviceEndDate { get; set; }
        [Display(Name = "Pickup Confirmed")]
        public bool pickupConfirmed { get; set; }
        [Display(Name = "Charge")]
        public double pickupCharge { get; set; }
        [Display(Name = "Ex. Pickup")]
        public bool extraPickupRequested { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ex. P. Date")]
        public DateTime extraPickupDate { get; set; }
        [Display(Name = "Ex. P. Confirmed")]
        public bool extraPickupConfirmed { get; set; }
        [Display(Name = "Ex. P. Chg")]
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
            //set
            //{
            //    value = 
            //}
        }

    }
}