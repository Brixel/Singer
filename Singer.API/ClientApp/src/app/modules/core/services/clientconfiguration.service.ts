import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subscription, of } from 'rxjs';
import { map } from 'rxjs/operators';
@Injectable({
   providedIn: 'root',
})
export class ConfigurationService {
   configuration: Configuration;
   constructor(
      private http: HttpClient
   ) {}
   load(): Observable<Configuration> {
      const jsonFile = `assets/config.json`;
      return this.http.get(jsonFile).pipe(map((res) => {
         this.configuration = res as Configuration;
         return this.configuration;
      }));
   }
}

export interface Configuration {
   client_id: string;
   client_secret: string;
   applicationinsights_intrumentationkey: string;
}
