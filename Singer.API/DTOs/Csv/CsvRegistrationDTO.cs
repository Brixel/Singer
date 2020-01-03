using CsvHelper.Configuration;
using Singer.Models;
using Singer.Resources;
using System;

namespace Singer.DTOs.Csv
{
   public class CsvRegistrationDTO
   {
      public string CaseNumber { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public DateTime BirthDay { get; set; }
      public AgeGroup AgeGroup { get; set; }
      public bool IsExtern { get; set; }
      public bool HasTrajectory { get; set; }
      
      public class Mapper : ClassMap<CsvRegistrationDTO>
      {
         public Mapper()
         {
            Map(x => x.CaseNumber).Index(0).Name(DisplayNames.CaseNumber);
            Map(x => x.FirstName).Index(1).Name(DisplayNames.FirstName);
            Map(x => x.LastName).Index(2).Name(DisplayNames.LastName);
            Map(x => x.BirthDay).TypeConverterOption.Format("dd/MM/yy").Index(3).Name(DisplayNames.BirthDay);
            Map(x => x.AgeGroup).Index(4).Name(DisplayNames.AgeGroup);
            Map(x => x.IsExtern).Index(5).Name(DisplayNames.IsExtern);
            Map(x => x.HasTrajectory).Index(6).Name(DisplayNames.HasTrajectory);
         }
      }
   }
}
