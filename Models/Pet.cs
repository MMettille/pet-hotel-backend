using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace pet_hotel
{
    public enum PetBreedType
    {
        Shepherd,
        Poodle,
        Beagle,
        Bulldog,
        Terrier,
        Boxer,
        Labrador,
        Retriever
    }
    public enum PetColorType 
    {
        White,
        Black,
        Golden,
        Tricolor,
        Spotted
    }
    public class Pet
    {
        public int id {get; set;}

        [Required]
        public string name {get; set;}

        [Required] 
        [JsonConverter(typeof(JsonStringEnumConverter))]  // dont want a number, get me the actual value   
        public PetBreedType breed {get; set;}

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]  // dont want a number, get me the actual value
        public PetColorType color {get; set;}

        public static DateTime Now { get; set;}

        public PetOwner petOwner {get; set;}
        
        [Required]
        [ForeignKey("PetOwner")]
        public int petOwnerid {get; set;}        
    }
}
