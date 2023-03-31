using System;

using Singer.DTOs;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Resources;
using Singer.Services.Interfaces;

namespace Singer.Services;

public class DateValidator : IDateValidator
{
    public void Validate(EventDTO dto) => ValidateEventDates(
       dto.StartRegistrationDateTime, dto.EndRegistrationDateTime,
       dto.HasDayCareBefore, dto.DayCareBeforeStartDateTime,
       dto.StartDateTime, dto.EndDateTime,
       dto.HasDayCareAfter, dto.DayCareAfterEndDateTime);

    public void Validate(CreateEventDTO dto) => ValidateEventDates(
       dto.StartRegistrationDateTime, dto.EndRegistrationDateTime,
       dto.HasDayCareBefore, dto.DayCareBeforeStartDateTime,
       dto.StartDateTime, dto.EndDateTime,
       dto.HasDayCareAfter, dto.DayCareAfterEndDateTime);

    public void Validate(UpdateEventDTO dto) => ValidateEventDates(
       dto.StartRegistrationDateTime, dto.EndRegistrationDateTime,
       dto.HasDayCareBefore, dto.DayCareBeforeStartDateTime,
       dto.StartDateTime, dto.EndDateTime,
       dto.HasDayCareAfter, dto.DayCareAfterEndDateTime);

    protected void ValidateEventDates(
       DateTime startRegistration, DateTime endRegistration,
       bool hasDayCareBefore, DateTime? dayCareBefore,
       DateTime start, DateTime end,
       bool hasDayCareAfter, DateTime? dayCareAfter)
    {
        if (startRegistration > endRegistration)
            throw new BadInputException("Invalid start/end registration date time", ErrorMessages.StartCannotBeforEndRegistrationEvent);
        if (startRegistration > start)
            throw new BadInputException("Invalid start registration date time", ErrorMessages.CannotRegistrateAfterStartEvent);

        if (start > end)
            throw new BadInputException("Invalid start/end date time", ErrorMessages.CannotStartEventAfterEndingIt);

        if (hasDayCareBefore)
        {
            if (dayCareBefore == null)
                throw new BadInputException("Invalid day care before end date time", ErrorMessages.StartDaycareBeforeNotEntered);
            if (dayCareBefore > start)
                throw new BadInputException("Invalid day care before end date time", ErrorMessages.CannotStartDaycareBeforeWhenEventIsBusy);
        }

        if (hasDayCareAfter)
        {
            if (dayCareAfter == null)
                throw new BadInputException("Invalid day care after end date time", ErrorMessages.StartDaycareAfterNotEntered);
            if (dayCareAfter < end)
                throw new BadInputException("Invalid day care after end date time", ErrorMessages.CannotStartDaycareAfterWhileEventIsBusy);
        }
    }

    public void Validate(CreateCareUserDTO dto) => ValidateCareUserDates(dto.BirthDay);

    public void Validate(UpdateCareUserDTO dto) => ValidateCareUserDates(dto.BirthDay);

    protected void ValidateCareUserDates(DateTime birthDay)
    {
        if (birthDay > DateTime.Now)
            throw new BadInputException("Invalid birthday", ErrorMessages.BirthDayInTheFuture);
    }
}
