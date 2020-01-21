using System;
using System.Collections.Generic;
using System.Linq;
using Singer.DTOs;
using Singer.DummyDataSeeder.Data.Bases;
using Singer.Helpers.Enums;
using Singer.Helpers.Extensions;
using Singer.Models;

namespace Singer.DummyDataSeeder.Data
{
    internal class Events : DataContainer<EventDTO, CreateEventDTO>
    {
        private readonly IList<EventLocationDTO> _eventLocations;

        private IDtoStorer<EventDTO, CreateEventDTO>[] _data;


        public Events(IList<EventLocationDTO> locations)
        {
            _eventLocations = locations;
        }

        public override IDtoStorer<EventDTO, CreateEventDTO>[] Data
        {
            get
            {
                if (_data != null)
                    return _data;

                var data = new List<IDtoStorer<EventDTO, CreateEventDTO>>();

                var start = NewStart().SetTime(new DateTime(0, 0, 0, 14, 0, 0));
                var end = start.AddHours(2);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Bezoek aan de Kluis van Bolderberg",
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Adult, AgeGroup.Youngster, AgeGroup.Child },
                        StartDateTime = start,
                        EndDateTime = end,
                        Cost = 0,
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "We gaan een wandeling maken van ongeveer 4km rond en op de Bolderberg. De Kluis van Bolderberg ligt boven op de Bolderberg in het dorp Bolderberg van de Belgische gemeente Heusden-Zolder. De pelgrim Lambert Hoelen uit Diepenbeek bezocht tijdens een tocht tussen 1670 en 1672 naar Rome de Santa Maria di Loreto.In het Italiaanse stadje Loreto vlak bij Ancona is de Basiliek van het Heilig Huis gebouwd rondom een huis dat volgens de overlevering het geboortehuis van de maagd Maria was.Het zou door een serie wonderen in de late 13e eeuw van Nazareth naar Loreto zijn overgebracht.",
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 30,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartRegistrationDateTime = start.AddDays(-28)
                    },
                }); ;

                start = NewStart().SetTime(new DateTime(0, 0, 0, 14, 0, 0));
                end = start.AddHours(2);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Segway rijden",
                        Cost = 60,
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Adult, AgeGroup.Youngster },
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "We gaan rijden met een segway bij IVENTURA Intensive Adventures.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 15,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28)
                    },
                }); ;

                start = NewStart().SetTime(new DateTime(0, 0, 0, 11, 0, 0));
                end = start.AddHours(7);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Tripje naar bokrijk",
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Adult, AgeGroup.Youngster, AgeGroup.Child, AgeGroup.Kindergartener, AgeGroup.Toddler },
                        Cost = 0,
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "Bokrijk is altijd een leuke plaats om eens naartoe te gaan.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 100,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });

                start = NewStart().SetTime(new DateTime(0, 0, 0, 14, 0, 0));
                end = start.AddHours(2);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Voetballen",
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Youngster, AgeGroup.Child },
                        Cost = 0,
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "We gaan voetballen.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 30,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });

                start = NewStart().SetTime(new DateTime(0, 0, 0, 14, 0, 0));
                end = start.AddHours(2);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "BasketBal",
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Youngster, AgeGroup.Child },
                        Cost = 0,
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "We gaan basketballen.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 30,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });

                start = NewStart().SetTime(new DateTime(0, 0, 0, 14, 0, 0));
                end = start.AddHours(2);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Honkbal",
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Youngster },
                        Cost = 0,
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "We gaan honkballen.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 100,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });

                start = NewStart().SetTime(new DateTime(0, 0, 0, 9, 0, 0));
                end = start.AddDays(5).AddHours(8);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Toverkamp",
                        Cost = 15,
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Child },
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "Toverkamp is een week lang leren hoe je kan toveren, goochelen en veel meer.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 40,
                        RegistrationOnDailyBasis = true,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });

                start = NewStart().SetTime(new DateTime(0, 0, 0, 14, 0, 0));
                end = start.AddHours(4);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Rolstoelrace",
                        Cost = 10,
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Adult, AgeGroup.Youngster, AgeGroup.Child },
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "Op deze rolstoel race gaan we kijken wie de snelste en behendigste rolstoelgebruiker is.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 100,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });

                start = NewStart().SetTime(new DateTime(0, 0, 0, 14, 0, 0));
                end = start.AddHours(3);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Bosspel",
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Youngster, AgeGroup.Child },
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "We gaan een groot bosspel spelen.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 50,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });

                start = NewStart().SetTime(new DateTime(0, 0, 0, 9, 0, 0));
                end = start.AddDays(3).AddHours(8);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Technispelen",
                        Cost = 10,
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Child },
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "Technispelen is een kamp waarbij je kennis leert maken met een heleboel soorten technologie.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 50,
                        RegistrationOnDailyBasis = true,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });

                start = NewStart().SetTime(new DateTime(0, 0, 0, 9, 0, 0));
                end = start.AddDays(4).AddHours(7);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Danskamp",
                        Cost = 30,
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Child },
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "Op danskamp gaan we verschillende soorten dans uitproberen leren.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 15,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });

                start = NewStart().SetTime(new DateTime(0, 0, 0, 14, 0, 0));
                end = start.AddHours(2);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Caligrafie",
                        Cost = 5,
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Adult, AgeGroup.Youngster, AgeGroup.Child },
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "Met caligrafie leer je schrijven op speciale en mooie manieren.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 20,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });

                start = NewStart().SetTime(new DateTime(0, 0, 0, 14, 0, 0));
                end = start.AddHours(4);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Japanse tuinen",
                        Cost = 6,
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Adult, AgeGroup.Youngster, AgeGroup.Child },
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "De japanse tuinen zijn prachtig. Daarom gaan we eens een kijkje nemen.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 50,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });

                start = NewStart().SetTime(new DateTime(0, 0, 0, 9, 0, 0));
                end = start.AddHours(9);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Plopsa indoor",
                        Cost = 22,
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Child, AgeGroup.Kindergartener, AgeGroup.Toddler },
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "Voor ieder wat wils in plopsa indoor. Een hele dag plezier maken",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 50,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    },
                });
 
                start = NewStart().SetTime(new DateTime(0, 0, 0, 10, 0, 0));
                end = start.AddHours(7);
                data.Add(new Event
                {
                    CreateDto = new CreateEventDTO
                    {
                        Title = "Kinderboerderij",
                        AllowedAgeGroups = new List<AgeGroup> { AgeGroup.Adult, AgeGroup.Youngster, AgeGroup.Child, AgeGroup.Kindergartener, AgeGroup.Toddler },
                        Cost = 0,
                        DayCareAfterEndDateTime = null,
                        DayCareBeforeStartDateTime = null,
                        Description = "Ken jij al de dieren op de boerderij al? Kom mee naar de kinderboerederij van Kiewiet en leer ze allemaal kennen.",
                        EndDateTime = end,
                        EndRegistrationDateTime = start.AddDays(-7),
                        FinalCancellationDateTime = start.AddDays(-5),
                        HasDayCareAfter = false,
                        HasDayCareBefore = false,
                        LocationId = R.Pick(_eventLocations).Id,
                        MaxRegistrants = 100,
                        RegistrationOnDailyBasis = false,
                        RepeatSettings = new RepeatSettingsDTO
                        {
                            Interval = 1,
                            IntervalUnit = TimeUnit.Day,
                            RepeatType = RepeatType.OnDate,
                            StopRepeatDate = end,
                        },
                        StartDateTime = start,
                        StartRegistrationDateTime = start.AddDays(-28),
                    }
                });

                _data = data.ToArray();
                return _data;
            }
        }

        public static DateTime NewStart() => R.NewDateTime(DateTime.Now.AddDays(50), DateTime.Now.AddDays(250));
    }
}
