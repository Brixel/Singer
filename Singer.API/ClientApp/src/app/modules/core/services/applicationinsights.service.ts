import { ConfigurationService } from './clientconfiguration.service';
import { Injectable } from '@angular/core';
import { AppInsightsService } from '@markpieszak/ng-application-insights';

@Injectable({
   providedIn: 'root',
})
export class ApplicationInsightsService {
   constructor(
      configurationService: ConfigurationService,
      appInsightsService: AppInsightsService
   ) {
      appInsightsService.config = {
         instrumentationKey: configurationService.configuration.applicationinsights_intrumentationkey
      };
      // then make sure to initialize and start-up app insights
      appInsightsService.init();
   }
}
