import { Component, Input, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { RegistrationStatus } from 'src/app/modules/core/models/enum';
import { MatButtonToggleChange } from '@angular/material/button-toggle';

@Component({
   selector: 'app-registration-status-toggle',
   templateUrl: './registration-status-toggle.component.html',
   styleUrls: ['./registration-status-toggle.component.css'],
})
export class RegistrationStatusToggleComponent {
   @Input('status') registrationStatus: RegistrationStatus;
   @Output() onStatusChange: EventEmitter<RegistrationStatus> = new EventEmitter();
   RegistrationStatus = RegistrationStatus;
   constructor() {}
   changeStatus(event: MatButtonToggleChange) {
      const registrationStatus = <RegistrationStatus>event.value;
      this.registrationStatus = registrationStatus;
      this.onStatusChange.emit(registrationStatus);
   }
}
