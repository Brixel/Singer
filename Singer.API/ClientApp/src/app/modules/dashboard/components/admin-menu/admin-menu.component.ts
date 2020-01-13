import { Component } from '@angular/core';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';

@Component({
   selector: 'app-admin-menu',
   templateUrl: './admin-menu.component.html',
   styleUrls: ['./admin-menu.component.css'],
})
export class AdminMenuComponent {

   startRegistrationDate: Date;
   endRegistrationDate: Date;
   finalCancelationDate: Date;
   startEventDate: Date;
   endEventDate: Date;
   constructor() {}

   buttonClick() {
      if(this.startRegistrationDate == null) {
         this.startRegistrationDate = new Date('11/01/2020');
         return;
      }

      if(this.endRegistrationDate == null) {
         this.endRegistrationDate = new Date('11/03/2020');
         return;
      }

      if(this.finalCancelationDate == null) {
         this.finalCancelationDate = new Date('11/05/2020');
         return;
      }

      if(this.startEventDate == null) {
         this.startEventDate = new Date('11/07/2020');
         return;
      }

      if(this.endEventDate == null) {
         this.endEventDate = new Date('11/08/2020');
         return;
      }


   }
}
