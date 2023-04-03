using System;

namespace Singer.Helpers.Enums;

[Flags]
public enum WeekDay
{
    Monday = 0b0000_0001,
    Tuesday = 0b0000_0010,
    Wednesday = 0b0000_0100,
    Thursday = 0b0000_1000,
    Friday = 0b0001_0000,
    Saturday = 0b0010_0000,
    Sunday = 0b0100_0000
}
