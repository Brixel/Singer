import { HttpClient, HttpParams, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Inject } from '@angular/core';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export class ApiService{
   error$: Subject<HttpErrorResponse>;
   constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string){}



   get(path: string, params: HttpParams = new HttpParams()): Observable<any> {
      return this.httpClient
          .get(`${this.baseUrl}${path}`, { params })
          .pipe(catchError((error) => this.handleError(error)));
  }

  put(path: string, body: Object = {}): Observable<any> {
      return this.httpClient
          .put(`${this.baseUrl}${path}`, JSON.stringify(body))
          .pipe(catchError((error) => this.handleError(error)));
  }

  post(path: string, body: Object = {}): Observable<any> {
      const httpOptions = {
          headers: new HttpHeaders({
              'Content-Type': 'application/json'
          })
      };

      return this.httpClient
          .post(`${this.baseUrl}${path}`, JSON.stringify(body), httpOptions)
          .pipe(catchError((error) => this.handleError(error)));
  }

  delete(path): Observable<any> {
      return this.httpClient
          .delete(`${this.baseUrl}${path}`)
          .pipe(catchError((error) => this.handleError(error)));
  }



  private handleError(error: HttpErrorResponse) {
   this.error$.next(error);
   return throwError(error.error);
}
}
