import { Component } from '@angular/core';
import {
   EventDescription,
   SearchEventDTO,
} from 'src/app/modules/core/models/singerevent.model';
import { Observable } from 'rxjs';
import { PaginationDTO } from 'src/app/modules/core/models/pagination.model';
import { HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ApiService } from 'src/app/modules/core/services/api.service';
import { SearchEventData } from '../event-search/event-search.component';
import { SingerEventLocation } from 'src/app/modules/core/models/singer-event-location.dto';
import { AuthService } from 'src/app/modules/core/services/auth.service';

@Component({
   selector: 'app-home',
   templateUrl: './home.component.html',
   styleUrls: ['./home.component.css'],
})
export class HomeComponent {
   constructor() {}
}
