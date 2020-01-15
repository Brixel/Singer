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
      public AgeGroup AgeGroup { get; set; }
      public bool IsExtern { get; set; }
      public DateTime? DayCareBeforeStartDateTime { get; set; }
      public DateTime? DayCareAfterEndDateTime { get; set; }
      public RegistrationStatus Status { get; set; }
      // TODO add legal guardian name
      //public string LegalGuardianName { get; set; }

      public class Mapper : ClassMap<CsvRegistrationDTO>
      {
         public Mapper()
         {
            Map(x => x.CaseNumber).Index(0).Name(DisplayNames.CaseNumber);
            Map(x => x.FirstName).Index(1).Name(DisplayNames.FirstName);
            Map(x => x.LastName).Index(2).Name(DisplayNames.LastName);
            Map(x => x.AgeGroup).Index(3).Name(DisplayNames.AgeGroup);
            Map(x => x.IsExtern).Index(4).Name(DisplayNames.IsExtern);
            Map(x => x.DayCareBeforeStartDateTime).TypeConverterOption.Format("dd/MM/yy hh:mm").Index(5).Name(DisplayNames.DayCareBeforeStartDateTime);
            Map(x => x.DayCareAfterEndDateTime).TypeConverterOption.Format("dd/MM/yy hh:mm").Index(6).Name(DisplayNames.DayCareAfterEndDateTime);
            Map(x => x.Status).Index(7).Name(DisplayNames.Status);
            // TODO add legal guardian name
            // Map(x => x.LegalGuardianName).Index(8).Name(DisplayNames.LegalGuardianUsers);
         }
      }
   }
}
