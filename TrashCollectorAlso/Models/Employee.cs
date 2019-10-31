using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrashCollectorAlso.Models
{
    public class Employee //tlc
    {
        [Key]
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string zip { get; set; }
        //tlc
        [ForeignKey("ApplicationUser")]//fk attr
        public string ApplicationId { get; set; } //fk's spot at the table
        public ApplicationUser ApplicationUser { get; set; }//the class the fk attr is referencing

    }
}