import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { AuthService } from 'src/app/modules/core/services/auth.service';
import { EventDescription } from 'src/app/modules/core/models/singerevent.model';
import { MatDrawer } from '@angular/material';

@Component({
   selector: 'app-home',
   templateUrl: './home.component.html',
   styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit{



   @ViewChild('drawer') drawer: MatDrawer;
   @Input() events: EventDescription[] = [];

   ngOnInit(): void {
      this.drawer.open();
   }
}
