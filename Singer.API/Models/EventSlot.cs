using System;
using System.Collections.Generic;
using Singer.DTOs;
using Singer.Helpers;
using Singer.Helpers.Enums;
using Singer.Helpers.Extensions;

namespace Singer.Models
{
   public class EventSlot : IIdentifiable
   {
      public Guid Id { get; set; }
      public Guid EventId { get; set; }
      public Event Event { get; set; }
      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }

      public IList<Registration> Registrations { get; set; }

      public static IEnumerable<EventSlot> GenerateEventSlotsUntilIncluding(DateTime start, DateTime end, DateTime until, TimeUnit interval)
      {
         Func<DateTime, DateTime> increase;
         switch (interval)
         {
            case TimeUnit.Day:
               increase = d => d.AddDays(1);
               break;
            case TimeUnit.Week:
               increase = d => d.AddDays(7);
               break;
            case TimeUnit.Month:
               increase = d => d.AddMonths(1);
               break;
            case TimeUnit.Year:
               increase = d => d.AddYears(1);
               break;
            default:
               yield break;
         }

         var duration = end - start;
         until = until.SetTime(start);
         for (var i = start; i <= until; i = increase(i))
            yield return new EventSlot { StartDateTime = i, EndDateTime = i + duration, };
      }

      public static IEnumerable<EventSlot> GenerateNumberOfEventSlots(DateTime start, DateTime end, int count, TimeUnit interval)
      {
         Func<DateTime, int, DateTime> increase;
         switch (interval)
         {
            case TimeUnit.Day:
               increase = (d, i) => d.AddDays(i);
               break;
            case TimeUnit.Week:
               increase = (d, i) => d.AddDays(i * 7);
               break;
            case TimeUnit.Month:
               increase = (d, i) => d.AddMonths(i);
               break;
            case TimeUnit.Year:
               increase = (d, i) => d.AddYears(i);
               break;
            default:
               yield break;
         }

         for (var i = 0; i < count; i++)
         {
            yield return new EventSlot
            {
               StartDateTime = increase(start, i),
               EndDateTime = increase(end, i),
            };
         }
      }
   }
}
