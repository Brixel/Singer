using Singer.Helpers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class EventLocationDTO : IIdentifiable
   {
      [Required]
      [DisplayName("Id")]
      public Guid Id { get; set; }

      [Required]
      [StringLength(maximumLength: 100,
         ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
         MinimumLength = 3)]
      [DisplayName("Naam")]
      public string Name { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Adres")]
      public string Address { get; set; }

      [Required]
      [StringLength(maximumLength: 10, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Postcode")]
      public string PostalCode { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Gemeente")]
      public string City { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Land")]
      public string Country { get; set; }
   }

   public class CreateEventLocationDTO
   {
      [Required]
      [StringLength(maximumLength: 100,
           ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
           MinimumLength = 3)]
      [DisplayName("Naam")]
      public string Name { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Adres")]
      public string Address { get; set; }

      [Required]
      [StringLength(maximumLength: 10, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Postcode")]
      public string PostalCode { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Gemeente")]
      public string City { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Land")]
      public string Country { get; set; }
   }

   public class UpdateEventLocationDTO
   {
      [Required]
      [StringLength(maximumLength: 100,
           ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
           MinimumLength = 3)]
      [DisplayName("Naam")]
      public string Name { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Adres")]
      public string Address { get; set; }

      [Required]
      [StringLength(maximumLength: 10, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Postcode")]
      public string PostalCode { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Gemeente")]
      public string City { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Land")]
      public string Country { get; set; }
   }
}
