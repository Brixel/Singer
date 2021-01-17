import { Injectable } from "@angular/core";
import { OwlDateTimeIntl } from "@danielmoncada/angular-datetime-picker";

// here is the default text string
@Injectable()
export class DutchOwlDateTimeIntl extends OwlDateTimeIntl {
   /** A label for the range 'from' in picker info */
   rangeFromLabel = 'Van';

   /** A label for the range 'to' in picker info */
   rangeToLabel = 'Tot';
}
