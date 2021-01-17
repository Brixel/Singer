import { HttpClient, HttpParams, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ApiService {
   error$ = new Subject<HttpErrorResponse>();
   constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

   get<T = any>(path: string, params: HttpParams = new HttpParams()): Observable<any> {
      return this.httpClient
         .get<T>(`${this.baseUrl}${path}`, { params })
         .pipe(catchError(error => this.handleError(error)));
   }

   put(path: string, body: Object = {}): Observable<any> {
      const httpOptions = {
         headers: new HttpHeaders({
            'Content-Type': 'application/json',
         }),
      };
      return this.httpClient
         .put(`${this.baseUrl}${path}`, JSON.stringify(body), httpOptions)
         .pipe(catchError(error => this.handleError(error)));
   }

   post(path: string, body: Object = {}): Observable<any> {
      const httpOptions = {
         headers: new HttpHeaders({
            'Content-Type': 'application/json',
         }),
      };

      return this.httpClient
         .post(`${this.baseUrl}${path}`, JSON.stringify(body), httpOptions)
         .pipe(catchError(error => this.handleError(error)));
   }

   delete(path): Observable<any> {
      return this.httpClient.delete(`${this.baseUrl}${path}`).pipe(catchError(error => this.handleError(error)));
   }

   downloadFile(path: string): Observable<Blob> {
      return this.httpClient
         .get(path, {
            responseType: 'blob',
         })
         .pipe(catchError(error => this.handleError(error)));
   }

   private handleError(error: HttpErrorResponse) {
      this.error$.next(error);
      console.error(error);
      return throwError(error.error);
   }
}
