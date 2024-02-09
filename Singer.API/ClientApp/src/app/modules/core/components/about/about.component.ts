import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject } from 'rxjs';

@Component({
   selector: 'app-about',
   templateUrl: './about.component.html',
   styleUrls: ['./about.component.css'],
})
export class AboutComponent {
   public uiVersion: string = environment.VERSION;

   private apiVersionSubject$ = new BehaviorSubject<string>(null);
   apiVersion$ = this.apiVersionSubject$.asObservable();

   private isAdminSubject$ = new BehaviorSubject<boolean>(false);
   isAdmin$ = this.isAdminSubject$.asObservable();

   constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      http.get<AboutDTO>(baseUrl + 'api/about').subscribe(
         (result) => {
            this.apiVersionSubject$.next(result.apiVersion);
            this.isAdminSubject$.next(result.userInfo.isAdmin);
         },
         (error) => console.error(error)
      );
   }
}

interface AboutDTO {
   apiVersion: string;
   userInfo: UserInfoDTO;
}

interface UserInfoDTO {
   isAdmin: true;
}
