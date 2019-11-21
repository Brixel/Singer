import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
   selector: 'app-user-card',
   templateUrl: './user-card.component.html',
   styleUrls: ['./user-card.component.css'],
})
export class UserCardComponent implements OnInit {
   @Input() user: any = {};
   @Output() delete = new EventEmitter<void>();
   constructor() {}

   ngOnInit() {}

   deleteUser() {
      this.delete.emit();
   }
}
