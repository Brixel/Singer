import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from 'src/app/modules/core/services/auth.service';
import { EventDescription } from 'src/app/modules/core/models/singerevent.model';

@Component({
   selector: 'app-home',
   templateUrl: './home.component.html',
   styleUrls: ['./home.component.css']
})

export class HomeComponent {
   @Input() events: EventDescription[] = [];
}
