using Singer.DTOs.Users;
using Singer.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class EventRegistrationDTO
   {
      [Required]
      [DisplayName("Id")]
      public Guid Id { get; set; }

      [Required]
      [DisplayName("Event slot")]
      public EventSlotDTO EventSlot { get; set; }

      [Required]
      [DisplayName("Beschrijving")]
      public EventDescriptionDTO EventDescription { get; set; }

      [Required]
      [DisplayName("Zorg gebruiker")]
      public CareUserDTO CareUser { get; set; }

      [Required]
      [DisplayName("Status")]
      public RegistrationStatus Status { get; set; }
   }

   public class CreateEventRegistrationDTO
   {
      [Required]
      [DisplayName("Event id")]
      public Guid EventId { get; set; }

      [Required]
      [DisplayName("Zorg gebruiker id")]
      public Guid CareUserId { get; set; }

      [Required]
      [DisplayName("Status")]
      public RegistrationStatus Status { get; set; }
   }

   public class UpdateEventRegistrationDTO
   {
      [Required]
      [DisplayName("Event slot id")]
      public Guid EventSlotId { get; set; }

      [Required]
      [DisplayName("Zorg gebruiker id")]
      public Guid CareUserId { get; set; }

      [Required]
      [DisplayName("Status")]
      public RegistrationStatus Status { get; set; }
   }
}
