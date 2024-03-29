using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Singer.DTOs.Users;
using Singer.Helpers;
using Singer.Models;
using Singer.Resources;

namespace Singer.DTOs;

public class RegistrationOverviewDTO : IIdentifiable
{
    public Guid Id { get; set; }
    public string EventTitle { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public RegistrationTypes RegistrationType { get; set; }
    public string CareUserFirstName { get; set; }
    public string CareUserLastName { get; set; }
    public RegistrationStatus RegistrationStatus { get; set; }
    public DaycareLocationDTO DaycareLocation { get; set; }
}
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

    public SingerLocationDTO DaycareLocation { get; set; }

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
    public List<Guid> CareUserIds { get; set; }
    public RegistrationTypes? RegistrationType { get; set; }
    public RegistrationStatus? RegistrationStatus { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}
