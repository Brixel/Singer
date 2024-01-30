/***************************************************************************************************
 * Load `$localize` onto the global scope - used if i18n tags appear in Angular templates.
 */
import '@angular/localize/init';
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { endpoint } from './app/modules/core/services/auth-config';

export function getBaseUrl(): string {
   return endpoint;
}

const providers = [{ provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }];

if (environment.production) {
   enableProdMode();
}

platformBrowserDynamic(providers)
   .bootstrapModule(AppModule)
   .catch((err) => console.error(err));
