import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
   selector: 'app-fetch-data',
   templateUrl: './fetch-data.component.html',
})
export class FetchDataComponent {
   public forecasts: WeatherForecast[];

   private baseUrl = 'https://localhost:5001/';
   constructor(http: HttpClient) {
      http
         .get<WeatherForecast[]>(this.baseUrl + 'api/SampleData/WeatherForecasts')
         .subscribe(
            result => {
               this.forecasts = result;
            },
            error => console.error(error)
         );
   }
}

interface WeatherForecast {
   dateFormatted: string;
   temperatureC: number;
   temperatureF: number;
   summary: string;
}
