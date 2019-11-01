using Singer.DTOs.Users;
using Singer.Models;
using Singer.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class EventRegistrationDTO
   {
      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Id))]
      public Guid Id { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.EventSlot))]
      public EventSlotDTO EventSlot { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.EventDescription))]
      public EventDescriptionDTO EventDescription { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.CareUser))]
      public CareUserDTO CareUser { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Status))]
      public RegistrationStatus Status { get; set; }
   }

   public class CreateEventRegistrationDTO
   {
      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.EventId))]
      public Guid EventId { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.CareUserId))]
      public Guid CareUserId { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Status))]
      public RegistrationStatus Status { get; set; }
   }

   public class UpdateEventRegistrationDTO
   {
      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.EventSlotId))]
      public Guid EventSlotId { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.CareUserId))]
      public Guid CareUserId { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Status))]
      public RegistrationStatus Status { get; set; }
   }
}
