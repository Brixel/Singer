import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { EventDescription } from 'src/app/modules/core/models/singerevent.model';
import { MatDrawer } from '@angular/material';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']
})
export class EventListComponent implements OnInit {

   @Input() events: EventDescription[];


   breakpoint: number;

  constructor() { }

   ngOnInit() {
      this.breakpoint = (window.innerWidth <= 500) ? 1 : 3;
   }

   onResize(event) {
      this.breakpoint = (event.target.innerWidth <= 500) ? 1 : 3;
   }

}
