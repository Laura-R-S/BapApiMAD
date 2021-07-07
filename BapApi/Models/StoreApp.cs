using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BapApi.Models
{

    /// <summary>
    /// DATE: 02/07/2021
    /// 
    /// [1] Database Generated
    /// The DatabaseGenerated attribute added to the properties 
    /// whose value is automatically computed/updated by the Database.
    /// It specifies how the database generates values for the property. 
    /// There are three possible values: Identity: Specifies that the column 
    /// is an identity column, which is typically used for integer primary keys.
    /// Computed: Specifies that the database generates the value for the column.
    /// 
    /// The advantages of auto incremented primary keys are numerous,
    /// but the most impactful benefits are faster speed when performing queries and 
    /// data-independence when searching through thousands of records which might 
    /// contain frequently altered data elsewhere in the table. With a consistent 
    /// and unique numeric identifier, applications can take advantage of these faster
    /// and more reliable queries.
    /// https://entityframework.net/database-generated#:~:text=Identity.%20When%20DatabaseGenerated.Identity%20applied%20to%20a%20property%2C%20the,by%20the%20database%20when%20the%20row%20is%20inserted.
    /// https://chartio.com/resources/tutorials/how-to-define-an-auto-increment-primary-key-in-sql-server/
    /// 
    /// </summary>
    public class StoreApp
    {
        // These are validation attributes let you specify validation rules for model properties. 
        // [Required]: Validates that the field is not null.
        // Validation attributes let you specify the error message to be displayed for invalid input.
       [Column("id")]
       [Display(Name = "ID")]
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       [Required(ErrorMessage = "ID is required")] 
       public int Id { get; set; }

        // [StringLength]: Validates that a string property value doesn't exceed a specified length limit.
       [Column("name")]
       [Display(Name = "Name")]
       [StringLength(100, MinimumLength = 3)]
       [Required(ErrorMessage = "Name must be between 3 and 100 characters")]
       public string Name { get; set; }

        // [RegularExpression]: Validates that the property value matches a specified regular expression.
        [Column("rating")]
        [Display(Name = "Rating")]
        [StringLength(5)]
        [RegularExpression("^[0-9]*$")]
        public double Rating { get; set; }


        [Column("people")]
        [Display(Name = "Number of People Rated")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int People { get; set; }


        [Column("category")]
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }



        [Column("date")]
        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date is required")]
        public string Date { get; set; }

        // [RegularExpression]: Validates that the property value matches a specified regular expression.
        [Column("price")]
        [Display(Name = "Price")]
        [RegularExpression("^[0-9]*$")]
        [Required(ErrorMessage = "Price should be a number")]
        public string Price  { get; set; }
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

    // created a new class for returning new structure
    public class StoreDetail {

        public List<StoreAppDTO> AppList { get; set; }
        public decimal TotalPageCount { get; set; }
    }

}