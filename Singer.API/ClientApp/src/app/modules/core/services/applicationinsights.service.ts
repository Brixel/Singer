import { ConfigurationService } from './clientconfiguration.service';
import { Injectable } from '@angular/core';
import { AppInsightsService } from '@markpieszak/ng-application-insights';

@Injectable({
   providedIn: 'root',
})
export class ApplicationInsightsService {
   constructor(
      private _configurationService: ConfigurationService,
      private _appInsightsService: AppInsightsService
   ) {}

   init() {
      this._appInsightsService.config = {
         instrumentationKey: this._configurationService.configuration
            .applicationinsights_intrumentationkey,
      };
      // then make sure to initialize and start-up app insights
      this._appInsightsService.init();
   }
}
