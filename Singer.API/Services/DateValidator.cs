using Singer.DTOs;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Services.Interfaces;
using System;

namespace Singer.Services
{
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
            throw new BadInputException("Invalid start/end registration date time", "De start tijd/datum om te registreren kan niet na de eind registratie datum liggen.");
         if (startRegistration > start)
            throw new BadInputException("Invalid start registration date time", "De start tijd/datum om te registreren kan liggen na de start tijd/datum van het evenement.");

         if (start > end)
            throw new BadInputException("Invalid start/end date time", "De start tijd/datum kan niet na het einde gebeuren.");

         if (hasDayCareBefore)
         {
            if (dayCareBefore == null)
               throw new BadInputException("Invalid day care before end date time", "De start tijd/datum voor de opvang vóór het evenement moet nog worden ingevuld.");
            if (dayCareBefore > start)
               throw new BadInputException("Invalid day care before end date time", "De opvang vóór het evenement kan niet starten als het evenement al bezig is.");
         }

         if (hasDayCareAfter)
         {
            if (dayCareAfter == null)
               throw new BadInputException("Invalid day care after end date time", "De start tijd/datum voor de opvang na het evenement moet nog worden ingevuld.");
            if (dayCareAfter < end)
               throw new BadInputException("Invalid day care after end date time", "De opvang na het evenement kan niet starten als het evenement nog bezig is.");
         }
      }

      public void Validate(CreateCareUserDTO dto) => ValidateCareUserDates(dto.BirthDay);

      public void Validate(UpdateCareUserDTO dto) => ValidateCareUserDates(dto.BirthDay);

      protected void ValidateCareUserDates(DateTime birthDay)
      {
         if (birthDay > DateTime.Now)
            throw new BadInputException("Invalid birthday", "De geboortedatum van de zorggebruiker kan niet na vandaag vallen.");
      }
   }
}
