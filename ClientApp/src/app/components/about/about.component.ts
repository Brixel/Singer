import { Component, OnInit, Inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
   selector: 'app-about',
   templateUrl: './about.component.html',
   styleUrls: ['./about.component.css'],
})
export class AboutComponent implements OnInit {
   ngOnInit(): void {
      throw new Error('Method not implemented.');
   }
   public uiVersion: string = environment.VERSION;

   public about: AboutModel;
   constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      http.get<AboutModel>(baseUrl + 'api/About').subscribe(
         result => {
            this.about = result;
         },
         error => console.error(error)
      );
   }
}

interface AboutModel {
   apiVersion: string;
}
