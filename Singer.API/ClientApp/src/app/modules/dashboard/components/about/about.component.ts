import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
   selector: 'app-about',
   templateUrl: './about.component.html',
   styleUrls: ['./about.component.css'],
})
export class AboutComponent {
   public uiVersion: string = environment.VERSION;

   public about: AboutDTO;
   constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      http.get<AboutDTO>(baseUrl + 'api/About').subscribe(
         result => {
            this.about = result;
         },
         error => console.error(error)
      );
   }
}

interface AboutDTO {
   apiVersion: string;
}
