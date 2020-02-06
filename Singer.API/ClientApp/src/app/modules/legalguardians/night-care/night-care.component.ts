import { Component, OnInit } from '@angular/core';
import {
   CalendarView,
   CalendarEvent,
   CalendarEventTimesChangedEvent,
   DAYS_OF_WEEK,
   CalendarDateFormatter,
   CalendarMomentDateFormatter,
} from 'angular-calendar';
import { WeekViewHourSegment } from 'calendar-utils';
import {
   startOfDay,
   endOfDay,
   subDays,
   addDays,
   endOfMonth,
   isSameDay,
   isSameMonth,
   addHours,
   endOfWeek,
   addMinutes,
} from 'date-fns';
import * as moment from 'moment';
import { fromEvent, Subject } from 'rxjs';
import { finalize, takeUntil } from 'rxjs/operators';

const colors: any = {
   red: {
      primary: '#ad2121',
      secondary: '#FAE3E3',
   },
   blue: {
      primary: '#1e90ff',
      secondary: '#D1E8FF',
   },
   yellow: {
      primary: '#e3bc08',
      secondary: '#FDF1BA',
   },
};

moment.updateLocale('nl-BE', {
   week: {
      dow: DAYS_OF_WEEK.MONDAY,
      doy: 0,
   },
   longDateFormat: {
      LT: 'HH:mm',
      LTS: 'HH:mm:ss',
      L: 'DD/MM/YYYY',
      LL: 'D MMMM YYYY',
      LLL: 'D MMMM YYYY HH:mm',
      LLLL: 'dddd D MMMM YYYY HH:mm',
   },
});

@Component({
   selector: 'app-night-care',
   templateUrl: './night-care.component.html',
   styleUrls: ['./night-care.component.css'],
})
export class NightCareComponent implements OnInit {
   view: CalendarView = CalendarView.Month;

   CalendarView = CalendarView;

   viewDate: Date = new Date();

   weekStartsOn = DAYS_OF_WEEK.MONDAY;

   dragToCreateActive = false;

   events: CalendarEvent[] = [
      {
         start: subDays(startOfDay(new Date()), 1),
         end: addDays(new Date(), 1),
         title: 'A 3 day event',
         color: colors.red,
         // actions: this.actions,
         allDay: true,
         resizable: {
            beforeStart: true,
            afterEnd: true,
         },
         draggable: true,
      },
      {
         start: startOfDay(new Date()),
         title: 'An event with no end date',
         color: colors.yellow,
         // actions: this.actions,
      },
      {
         start: subDays(endOfMonth(new Date()), 3),
         end: addDays(endOfMonth(new Date()), 3),
         title: 'A long event that spans 2 months',
         color: colors.blue,
         allDay: true,
      },
      {
         start: addHours(startOfDay(new Date()), 2),
         end: addHours(new Date(), 2),
         title: 'A draggable and resizable event',
         color: colors.yellow,
         // actions: this.actions,
         resizable: {
            beforeStart: true,
            afterEnd: true,
         },
         draggable: true,
      },
   ];

   activeDayIsOpen = false;

   constructor() {}

   ngOnInit() {}

   setViewDateToToday() {
      this.viewDate = new Date();
   }

   setMonthView() {
      this.view = CalendarView.Month;
   }

   goToPreviousMonth() {
      const newDate = moment(this.viewDate)
         .subtract(1, 'months')
         .toDate();
      this.viewDate = newDate;
   }

   goToNextMonth() {
      const newDate = moment(this.viewDate)
         .add(1, 'months')
         .toDate();
      this.viewDate = newDate;
   }

   dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
      this.viewDate = date;
      this.view = CalendarView.Day;
   }

   private floorToNearest(amount: number, precision: number) {
      return Math.floor(amount / precision) * precision;
   }

   private ceilToNearest(amount: number, precision: number) {
      return Math.ceil(amount / precision) * precision;
   }

   startDragToCreate(segment: WeekViewHourSegment, mouseDownEvent: MouseEvent, segmentElement: HTMLElement) {
      const dragToSelectEvent: CalendarEvent = {
         id: this.events.length,
         title: 'New event',
         start: segment.date,
         meta: {
            tmpEvent: true,
         },
         actions: [
            {
               label: '<mat-icon>delete</mat-icon>',
               onClick: ({ event }: { event: CalendarEvent }): void => {
                  this.events = this.events.filter(iEvent => iEvent !== event);
                  console.log('Event deleted', event);
               },
            },
         ],
         resizable: {
            beforeStart: true, // this allows you to configure the sides the event is resizable from
            afterEnd: true,
         },
      };
      this.events = [...this.events, dragToSelectEvent];
      const segmentPosition = segmentElement.getBoundingClientRect();
      this.dragToCreateActive = true;
      const endOfView = endOfWeek(this.viewDate, {
         weekStartsOn: this.weekStartsOn,
      });

      fromEvent(document, 'mousemove')
         .pipe(
            finalize(() => {
               delete dragToSelectEvent.meta.tmpEvent;
               this.dragToCreateActive = false;
               this.refresh();
            }),
            takeUntil(fromEvent(document, 'mouseup'))
         )
         .subscribe((mouseMoveEvent: MouseEvent) => {
            const minutesDiff = this.ceilToNearest(mouseMoveEvent.clientY - segmentPosition.top, 30);

            const daysDiff =
               this.floorToNearest(mouseMoveEvent.clientX - segmentPosition.left, segmentPosition.width) /
               segmentPosition.width;

            const newEnd = addDays(addMinutes(segment.date, minutesDiff), daysDiff);
            if (newEnd > segment.date && newEnd < endOfView) {
               dragToSelectEvent.end = newEnd;
            }
            this.refresh();
         });
   }

   refresh() {
      this.events = [...this.events];
   }

   eventTimesChanged({ event, newStart, newEnd }: CalendarEventTimesChangedEvent): void {
      event.start = newStart;
      event.end = newEnd;
      this.refresh();
   }

   handleEvent(action: string, event: CalendarEvent): void {
      console.log(event);
      // this.modalData = { event, action };
      // this.modal.open(this.modalContent, { size: 'lg' });
   }

   // addEvent(): void {
   //    this.events = [
   //       ...this.events,
   //       {
   //          title: 'New event',
   //          start: startOfDay(new Date()),
   //          end: endOfDay(new Date()),
   //          color: colors.red,
   //          draggable: true,
   //          resizable: {
   //             beforeStart: true,
   //             afterEnd: true,
   //          },
   //       },
   //    ];
   // }

   deleteEvent(eventToDelete: CalendarEvent) {
      this.events = this.events.filter(event => event !== eventToDelete);
   }

   setView(view: CalendarView) {
      this.view = view;
   }
}
