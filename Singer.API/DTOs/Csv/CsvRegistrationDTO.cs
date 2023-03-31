using System;
using System.Linq;

using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Resources;

namespace Singer.DTOs.Csv;

public class CsvRegistrationDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AgeGroup AgeGroup { get; set; }
    public bool IsExtern { get; set; }
    public DateTime? DayCareBeforeStartDateTime { get; set; }
    public DateTime? DayCareAfterEndDateTime { get; set; }
    public RegistrationStatus Status { get; set; }

    public class Mapper : ClassMap<CsvRegistrationDTO>
    {
        public Mapper()
        {
            _ = Map(x => x.FirstName).Index(1).Name(DisplayNames.FirstName);
            _ = Map(x => x.LastName).Index(2).Name(DisplayNames.LastName);
            _ = Map(x => x.AgeGroup).Index(3).Name(DisplayNames.AgeGroup).TypeConverter(new EnumTypeConverter<AgeGroup>());
            _ = Map(x => x.IsExtern).Index(4).Name(DisplayNames.IsExtern).TypeConverter(new IsExternBoolTypeConverter());
            _ = Map(x => x.DayCareBeforeStartDateTime).TypeConverterOption.Format("dd/MM/yy hh:mm").Index(5).Name(DisplayNames.DayCareBeforeStartDateTime);
            _ = Map(x => x.DayCareAfterEndDateTime).TypeConverterOption.Format("dd/MM/yy hh:mm").Index(6).Name(DisplayNames.DayCareAfterEndDateTime);
            _ = Map(x => x.Status).Index(7).Name(DisplayNames.Status).TypeConverter(new EnumTypeConverter<RegistrationStatus>());
        }

        public class EnumTypeConverter<T> : ITypeConverter where T : struct, Enum
        {
            public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                return Enum.TryParse<T>(text, out var ageGroup)
                   ? ageGroup
                   : (object)Enum.GetValues(typeof(T))
                      .Cast<T>()
                      .Select(x => new
                      {
                          Name = x.GetDisplayName(),
                          Value = x
                      })
                      .FirstOrDefault(x => x.Name == text)
                      .Value;
            }

            public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
            {
                return value is not T ageGroup
                   ? default
                   : ageGroup.GetDisplayName();
            }
        }

        public class IsExternBoolTypeConverter : ITypeConverter
        {
            public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) => !string.IsNullOrEmpty(text);

            public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) => value is not bool b || !b ? "" : "x";
        }
    }
}
