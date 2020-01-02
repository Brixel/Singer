using Singer.Helpers.Enums;
using Singer.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class RepeatSettingsDTO
   {
      [Range(
         minimum: 0,
         maximum: int.MaxValue,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Interval))]
      public int Interval { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.IntervalUnit))]
      public TimeUnit IntervalUnit { get; set; }

      [Display(
          ResourceType = typeof(DisplayNames),
          Name = nameof(DisplayNames.WeekRepeatMoment))]
      public WeekDay WeekRepeatMoment { get; set; }

      [Display(
          ResourceType = typeof(DisplayNames),
          Name = nameof(DisplayNames.MonthRepeatMoment))]
      public MonthRepeatMoment MonthRepeatMoment { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.RepeatType))]
      public RepeatType RepeatType { get; set; }

      [Range(
         minimum: 0,
         maximum: int.MaxValue,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.NumberOfRepeats))]
      public int NumberOfRepeats { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.StopRepeatDate))]
      public DateTime StopRepeatDate { get; set; }
   }
}
