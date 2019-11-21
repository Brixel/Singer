import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
   selector: 'app-detailed-user-card',
   templateUrl: './detailed-user-card.component.html',
   styleUrls: ['./detailed-user-card.component.css'],
})
export class DetailedUserCardComponent implements OnInit {
   @Input() user: any = {};
   @Input() colorOption: number;
   @Output() edit = new EventEmitter<void>();
   @Output() delete = new EventEmitter<void>();

   constructor() {}

   ngOnInit() {
      this.colorOption = Math.abs(Math.floor(this.colorOption)) % 10;
   }

   editUser() {
      this.edit.emit();
   }

   deleteUser() {
      this.delete.emit();
   }
}
