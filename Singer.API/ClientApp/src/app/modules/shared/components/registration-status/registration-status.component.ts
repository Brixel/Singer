import { Component, OnInit, Input } from '@angular/core';
import { RegistrationStatus } from 'src/app/modules/core/models/enum';

@Component({
   selector: 'app-registration-status',
   templateUrl: './registration-status.component.html',
   styleUrls: ['./registration-status.component.css'],
})
export class RegistrationStatusComponent implements OnInit {
   @Input('status') registrationStatus: RegistrationStatus;
   RegistrationStatus = RegistrationStatus;
   constructor() {}

   ngOnInit() {}
}
