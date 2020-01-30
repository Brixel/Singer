import { HttpErrorResponse } from '@angular/common/http';
import { Subject } from 'rxjs';

const error$ = new Subject<HttpErrorResponse>();
export const apiServiceMock = {
   error$,
   get: () => {},
   put: () => {},
   post: () => {},
   delete: () => {},
   uploadFile: () => {},
   downloadFile: () => {},
};
