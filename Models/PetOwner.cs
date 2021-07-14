using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pet_hotel
{
    public class PetOwner
    {
        // EF knows that id is the Primary Key, EF knows that is Serial
        public int id {get; set;}

        [Required]

        public string name {get; set;}


        [Required]

        public string emailAddress {get; set;}

        [NotMapped]
        public int petCount {get; set;}
    }
}
