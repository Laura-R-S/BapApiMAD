using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BapApi.Models
{
    public class StoreApp
    {
       [Column("id")]
       [Display(Name = "ID")]
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       [Required(ErrorMessage = "ID is required")] 
       public int      Id        { get; set; }


       [Column("name")]
       [Display(Name = "Name")]
       [StringLength(100, MinimumLength = 3)]
       [Required(ErrorMessage = "Name must be between 3 and 100 characters")]
       public string   Name      { get; set; }


       [Column("rating")]
       [Display(Name = "Rating")]
       [StringLength(5)]
       [RegularExpression("^[0-9]*$")]
       public double   Rating    { get; set; }


       public int      People    { get; set; }
       public string   Category  { get; set; }
       public string   Date      { get; set; }
       public string   Price     { get; set; }
    }

    /// <summary>
    /// DATE: 02/07/2021
    /// 
    /// [1] Data transfer object (DTO)
    /// To prevent our web API exposing the database entities to the client.
    /// Such as the client receiving data that is mapping directly to our
    /// database tables. Which is not a good idea thus we will create the StoreAppDTO.
    /// Data Transfer Object (DTO) to send secured data over the network.
    /// 
    /// In programming the data transfer object (DTO) is an object 
    /// that carries data between processes. The motivation for its use is that 
    /// communication between processes is usually done resorting to remote interfaces 
    /// (e.g., web services), where each call is an expensive operation
    /// because the majority of the cost of each call is related to the round-trip 
    /// time between the client and the server, one way of reducing the number of calls 
    /// is to use an object (the DTO) that aggregates the data that would have been 
    /// transferred by the several calls, but that is served by one call only.
    /// 
    /// The difference between data transfer objects and business objects or data 
    /// access objects is that a DTO does not have any behavior except for storage, 
    /// retrieval, serialization and deserialization of its own data(mutators, accessors,
    /// parsers and serializers). In other words, DTOs are simple objects that should not
    /// contain any business logic but may contain serialization and deserialization 
    /// mechanisms for transferring data over the wire. 
    /// https://en.wikipedia.org/wiki/Data_transfer_object
    /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
    /// </summary>
    public class StoreAppDTO
    {
        public int     Id        { get; set; }
        public string  Name      { get; set; }
        public double  Rating    { get; set; }
        public int     People    { get; set; }
        public string  Category  { get; set; }
        public string  Date      { get; set; }
        public string  Price     { get; set; }

    }

}