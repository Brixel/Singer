import { Component } from '@angular/core';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';

@Component({
   selector: 'app-admin-menu',
   templateUrl: './admin-menu.component.html',
   styleUrls: ['./admin-menu.component.css'],
})
export class AdminMenuComponent {

   startRegistrationDate: Date = new Date('11/01/2020');
   endRegistrationDate: Date = new Date('13/01/2020');
   finalCancelationDate: Date = new Date('15/01/2020');
   startEventDate: Date = new Date('20/01/2020');
   endEventDate: Date = new Date('21/01/2020');
   constructor() {}
}
