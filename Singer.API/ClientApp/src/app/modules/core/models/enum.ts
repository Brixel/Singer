export enum AgeGroup {
   Toddler = 1,
   Kindergartener = 2,
   Child = 4,
   Youngster = 8,
   Adult = 16,
}

export enum RegistrationStatus {
   Pending = 1,
   Accepted = 2,
   Rejected = 4,
}

export enum TimeUnit {
   Day = 0,
   Week = 1,
   Month = 2,
   Year = 3,
}

export enum WeekDay {
   Monday = 0b0000_0001,
   Tuesday = 0b0000_0010,
   Wednesday = 0b0000_0100,
   Thursday = 0b0000_1000,
   Friday = 0b0001_0000,
   Saturday = 0b0010_0000,
   Sunday = 0b0100_0000,
}

export enum MonthRepeatMoment {
   DayOfTheMonth = 0,
   WeekdayOfTheMonth = 1,
   WeekOfTheMonth = 2,
}

export enum RepeatType {
   OnDate = 0,
   AfterXTimes = 1,
}

export enum CostFilterParameter {
   Free = 0,
   UpToFive = 1,
   UpToTen = 2,
   UpToFifteen = 3,
   UpToTwentyFive = 4,
   UpToFifty = 5,
}
