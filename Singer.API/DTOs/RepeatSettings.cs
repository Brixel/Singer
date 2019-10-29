using Singer.Helpers.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   // TODO use all settings
   public class RepeatSettings
   {
      [Range(0, int.MaxValue)]
      [DisplayName("Interval")]
      public int Interval { get; set; }

      [DisplayName("Interval eenheid")]
      public TimeUnit IntervalUnit { get; set; }

      [DisplayName("Moment van de week")]
      public WeekDay WeekRepeatMoment { get; set; }

      [DisplayName("Moment van de maant")]
      public MonthRepeatMoment MonthRepeatMoment { get; set; }

      [DisplayName("Manier van herhalen")]
      public RepeatType RepeatType { get; set; }

      [Range(0, int.MaxValue)]
      [DisplayName("Aantal herhalingen")]
      public int NumberOfRepeats { get; set; }

      [DisplayName("Stop herhaaldatum")]
      public DateTime StopRepeatDate { get; set; }
   }
}
