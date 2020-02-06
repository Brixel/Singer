using Singer.DTOs.Users;
using Singer.Helpers;
using Singer.Models;
using Singer.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class RegistrationDTO : IIdentifiable
   {
      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Id))]
      public Guid Id { get; set; }

      public RegistrationTypes RegistrationType { get; set; }

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

      public EventLocationDTO DaycareLocation { get; set; }

      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Status))]
      public RegistrationStatus Status { get; set; }
   }

   public class CreateRegistrationDTO
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

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Status))]
      public RegistrationStatus? Status { get; set; }
   }

   public class CreateEventSlotRegistrationDTO
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

      [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.Status))]
      public RegistrationStatus? Status { get; set; }
   }

   public class UpdateRegistrationDTO
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

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Status))]
      public RegistrationStatus? Status { get; set; }
   }

   public class RegistrationSearchDTO : SearchDTOBase
   {

   }
}
