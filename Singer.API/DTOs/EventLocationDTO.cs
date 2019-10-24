using System;
using Singer.Helpers;

namespace Singer.DTOs
{
   public class EventLocationDTO : IIdentifiable
   {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public string Address { get; set; }
      public string PostalCode { get; set; }
      public string City { get; set; }
      public string Country { get; set; }
   }

   public class CreateEventLocationDTO
   {
      public string Name { get; set; }
      public string Address { get; set; }
      public string PostalCode { get; set; }
      public string City { get; set; }
      public string Country { get; set; }
   }

   public class UpdateEventLocationDTO
   {
      public string Name { get; set; }
      public string Address { get; set; }
      public string PostalCode { get; set; }
      public string City { get; set; }
      public string Country { get; set; }
   }
}
