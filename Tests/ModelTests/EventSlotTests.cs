using FluentAssertions;
using NUnit.Framework;
using Singer.DTOs;
using Singer.Models;
using System;
using System.Linq;

namespace Tests.ModelTests
{
   [TestFixture]
   public class EventSlotTests
   {
      #region GenerateEventSlotsUntil

      [Test]
      public void GenerateEventSlotsUntil()
      {
         var start = DateTime.Parse("2019-01-01T14:00:00+00:00");
         var end = DateTime.Parse("2019-01-01T16:00:00+00:00");
         var until = DateTime.Parse("2019-01-16");
         var slots = EventSlot.GenerateEventSlotsUntil(start, end, until, TimeUnit.Day).ToList();

         slots.Count
            .Should()
            .Be(15, "The event should repeat from the 1st until the 15th");

         slots[0].StartDateTime.Should()
            .Be(start, "that is the start time");

         slots[0].EndDateTime.Should()
            .Be(end, "that is the end time");

         slots[14].StartDateTime.Should()
            .Be(DateTime.Parse("2019-01-15T14:00:00+00:00"), "the date should increase but the time not");

         slots[14].EndDateTime.Should()
            .Be(DateTime.Parse("2019-01-15T16:00:00+00:00"), "the date should increase but the time not");
      }

      [Test]
      public void GenerateEventSlotsUntilFromMidnightToMidnight()
      {
         var start = DateTime.Parse("2018-12-31T00:00:00+00:00");
         var end = DateTime.Parse("2019-01-01T00:00:00+00:00");
         var until = DateTime.Parse("2019-01-15");
         var slots = EventSlot.GenerateEventSlotsUntil(start, end, until, TimeUnit.Day).ToList();

         slots.Count
            .Should()
            .Be(15, "The event should repeat from the 31st until the 15th");

         slots[0].StartDateTime.Should()
            .Be(start, "that is the start time");

         slots[0].EndDateTime.Should()
            .Be(end, "that is the end time");

         slots[14].StartDateTime.Should()
            .Be(DateTime.Parse("2019-01-14T00:00:00+00:00"), "the date should increase but the time not");

         slots[14].EndDateTime.Should()
            .Be(DateTime.Parse("2019-01-15T00:00:00+00:00"), "the date should increase but the time not");
      }

      [Test]
      public void GenerateEventSlotsUntilAroundMidnight()
      {
         var start = DateTime.Parse("2018-12-31T20:00:00+00:00");
         var end = DateTime.Parse("2019-01-01T06:00:00+00:00");
         var until = DateTime.Parse("2019-01-15");
         var slots = EventSlot.GenerateEventSlotsUntil(start, end, until, TimeUnit.Day).ToList();

         slots.Count
            .Should()
            .Be(15, "The event should repeat from the 31st until the 15th");

         slots[0].StartDateTime.Should()
            .Be(start, "that is the start time");

         slots[0].EndDateTime.Should()
            .Be(end, "that is the end time");

         slots[14].StartDateTime.Should()
            .Be(DateTime.Parse("2019-01-14T20:00:00+00:00"), "the date should increase but the time not");

         slots[14].EndDateTime.Should()
            .Be(DateTime.Parse("2019-01-15T06:00:00+00:00"), "the date should increase but the time not");
      }

      [Test]
      public void GenerateEventSlotsUntilWithMonth()
      {
         var start = DateTime.Parse("2019-01-15T14:00:00+00:00");
         var end = DateTime.Parse("2019-01-15T16:00:00+00:00");
         var until = DateTime.Parse("2019-12-16");
         var slots = EventSlot.GenerateEventSlotsUntil(start, end, until, TimeUnit.Month).ToList();

         slots.Count
            .Should()
            .Be(12, "The event should repeat from january to december");

         slots[0].StartDateTime.Should()
            .Be(start, "that is the start time");

         slots[0].EndDateTime.Should()
            .Be(end, "that is the end time");

         slots[11].StartDateTime.Should()
            .Be(DateTime.Parse("2019-12-15T14:00:00+00:00"), "the date should increase but the time not");

         slots[11].EndDateTime.Should()
            .Be(DateTime.Parse("2019-12-15T16:00:00+00:00"), "the date should increase but the time not");
      }

      #endregion GenerateEventSlotsUntil


      #region GenerateNumberOfEventSlots

      [Test]
      public void GenerateNumberOfEventSlots()
      {
         var start = DateTime.Parse("2019-01-01T14:00:00+00:00");
         var end = DateTime.Parse("2019-01-01T16:00:00+00:00");
         var count = 15;
         var slots = EventSlot.GenerateNumberOfEventSlots(start, end, count, TimeUnit.Day).ToList();

         slots.Count
            .Should()
            .Be(15, "The event should repeat 15 times");

         slots[0].StartDateTime.Should()
            .Be(start, "that is the start time");

         slots[0].EndDateTime.Should()
            .Be(end, "that is the end time");

         slots[14].StartDateTime.Should()
            .Be(DateTime.Parse("2019-01-15T14:00:00+00:00"), "the date should increase but the time not");

         slots[14].EndDateTime.Should()
            .Be(DateTime.Parse("2019-01-15T16:00:00+00:00"), "the date should increase but the time not");
      }

      [Test]
      public void GenerateNumberOfEventSlotsFromMidnightToMidnight()
      {
         var start = DateTime.Parse("2018-12-31T00:00:00+00:00");
         var end = DateTime.Parse("2019-01-01T00:00:00+00:00");
         var count = 15;
         var slots = EventSlot.GenerateNumberOfEventSlots(start, end, count, TimeUnit.Day).ToList();

         slots.Count
            .Should()
            .Be(15, "The event should repeat 15 times");

         slots[0].StartDateTime.Should()
            .Be(start, "that is the start time");

         slots[0].EndDateTime.Should()
            .Be(end, "that is the end time");

         slots[14].StartDateTime.Should()
            .Be(DateTime.Parse("2019-01-14T00:00:00+00:00"), "the date should increase but the time not");

         slots[14].EndDateTime.Should()
            .Be(DateTime.Parse("2019-01-15T00:00:00+00:00"), "the date should increase but the time not");
      }

      [Test]
      public void GenerateNumberOfEventSlotsAroundMidnight()
      {
         var start = DateTime.Parse("2018-12-31T20:00:00+00:00");
         var end = DateTime.Parse("2019-01-01T06:00:00+00:00");
         var count = 15;
         var slots = EventSlot.GenerateNumberOfEventSlots(start, end, count, TimeUnit.Day).ToList();

         slots.Count
            .Should()
            .Be(15, "The event should repeat 15 times");

         slots[0].StartDateTime.Should()
            .Be(start, "that is the start time");

         slots[0].EndDateTime.Should()
            .Be(end, "that is the end time");

         slots[14].StartDateTime.Should()
            .Be(DateTime.Parse("2019-01-14T20:00:00+00:00"), "the date should increase but the time not");

         slots[14].EndDateTime.Should()
            .Be(DateTime.Parse("2019-01-15T06:00:00+00:00"), "the date should increase but the time not");
      }

      [Test]
      public void GenerateNumberOfEventSlotsWithMonth()
      {
         var start = DateTime.Parse("2019-01-15T14:00:00+00:00");
         var end = DateTime.Parse("2019-01-15T16:00:00+00:00");
         var count = 12;
         var slots = EventSlot.GenerateNumberOfEventSlots(start, end, count, TimeUnit.Month).ToList();

         slots.Count
            .Should()
            .Be(12, "The event should repeat 12 times");

         slots[0].StartDateTime.Should()
            .Be(start, "that is the start time");

         slots[0].EndDateTime.Should()
            .Be(end, "that is the end time");

         slots[11].StartDateTime.Should()
            .Be(DateTime.Parse("2019-12-15T14:00:00+00:00"), "the date should increase but the time not");

         slots[11].EndDateTime.Should()
            .Be(DateTime.Parse("2019-12-15T16:00:00+00:00"), "the date should increase but the time not");
      }

      #endregion GenerateNumberOfEventSlots
   }
}
