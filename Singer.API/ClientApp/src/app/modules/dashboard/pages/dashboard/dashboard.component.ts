import { Component, OnInit } from '@angular/core';
import { SingerEvent, EventDescription } from 'src/app/modules/core/models/singerevent.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {


   events: EventDescription[];

  constructor() { }

  ngOnInit() {
  }

}
