using System;

namespace Singer.DTOs
{
   public class RepeatSettings
   {
      public int Interval { get; set; }
      public TimeUnit IntervalUnit { get; set; }

      public WeekDay WeekRepeatMoment { get; set; }
      public MonthRepeatMoment MonthRepeatMoment { get; set; }

      public RepeatType RepeatType { get; set; }

      public int NumberOfRepeats { get; set; }
      public DateTime StopRepeatDate { get; set; }
   }
}
