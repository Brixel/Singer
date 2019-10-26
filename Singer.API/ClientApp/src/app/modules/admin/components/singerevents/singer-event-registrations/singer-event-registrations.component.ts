import { Component, OnInit } from '@angular/core';
import { SingerEvent } from 'src/app/modules/core/models/singerevent.model';
import { FormGroup } from '@angular/forms';
import { CareUser } from 'src/app/modules/core/models/careuser.model';

export class SingerEventRegistrationData {
   event: SingerEvent;
}

@Component({
   selector: 'app-singer-event-registrations',
   templateUrl: './singer-event-registrations.component.html',
   styleUrls: ['./singer-event-registrations.component.css'],
})
export class SingerEventRegistrationsComponent implements OnInit {
   registrants = ['Jefke', 'Maria'];
   formGroup: FormGroup;

   constructor() {
      this.formGroup = new FormGroup({});
   }

   ngOnInit() {}

   userSelected(careUser: CareUser) {
      const newRegistrant = `${careUser.firstName} ${careUser.lastName}`;
      if (!this.registrants.includes(newRegistrant)) {
         this.registrants.push(newRegistrant);
      } else {
         // TODO Add notification when user already assigned
      }
   }
}
