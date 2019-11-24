namespace Singer.Resources
{
   public static class ValidationValues
   {
      public const int MaxNameLength = 100;
      public const int MinNameLength = 2;

      public const int MaxEmailLength = 255;

      public const int MaxAddressLength = 100;
      public const int MaxPostalCodeLength = 10;
      public const int MaxCityLength = 100;
      public const int MaxCountryLength = 100;

      public const int CaseNumberLength = 10;

      public const int MaxEventTitleLength = 100;
      public const int MinEventTitleLength = 2;

      public const int MaxDescriptionLength = 10000;

      public const int MaxMaxRegistrants = int.MaxValue;
      public const int MinMaxRegistrants = 1;

      public const int MaxCurrentRegistrants = int.MaxValue;
      public const int MinCurrentRegistrants = 0;

      public const int MaxEventCost = int.MaxValue;
      public const int MinEventCost = 0;

      public const int MaxUrlLength = 2048;
   }
}
