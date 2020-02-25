import { Component, Input, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { RegistrationStatus } from 'src/app/modules/core/models/enum';
import { MatButtonToggleChange } from '@angular/material';

@Component({
   selector: 'app-registration-status-toggle',
   templateUrl: './registration-status-toggle.component.html',
   styleUrls: ['./registration-status-toggle.component.css'],
})
export class RegistrationStatusToggleComponent implements AfterViewInit {
   @Input('status') registrationStatus: RegistrationStatus;
   @Output() onStatusChange: EventEmitter<RegistrationStatus> = new EventEmitter();
   RegistrationStatus = RegistrationStatus;
   constructor() {}
   changeStatus(event: MatButtonToggleChange) {
      console.log(event);
      const registrationStatus = <RegistrationStatus>event.value;
      this.registrationStatus = registrationStatus;
      console.log(`emitting: ${registrationStatus}`);
      this.onStatusChange.emit(registrationStatus);
   }
   ngOnInit() {}
   ngAfterViewInit(): void {
      console.log(this.registrationStatus);
   }
}
