import { Component, OnInit } from '@angular/core';
import { SingerEvent, EventDescription } from 'src/app/modules/core/models/singerevent.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor() { }

  ngOnInit() {
  }

}
