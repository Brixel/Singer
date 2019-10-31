import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
@Injectable({
   providedIn: 'root',
})
export class AuthService {
   private tokenURL = this.baseUrl + 'connect/token';
   private userInfoURL = this.baseUrl + 'connect/userinfo';

   private isAdminSubject = new Subject<boolean>();
   isAdmin$ = this.isAdminSubject.asObservable();

   private isAuthenticatedSubject = new Subject<boolean>();
   isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

   constructor(
      private http: HttpClient,
      private jwtHelper: JwtHelperService,
      @Inject('BASE_URL') private baseUrl: string) {}

   getUserInfo(): Observable<any> {
      return this.http.get(this.userInfoURL).pipe(map(res => res));
   }

   authenticate(username: string, password: string): Observable<any> {
      const headers = new HttpHeaders({
         'Content-Type': 'application/x-www-form-urlencoded',
      });
      const body = new URLSearchParams();
      body.set('username', username);
      body.set('password', password);
      body.set('grant_type', 'password');
      body.set('client_id', 'singer.client');
      body.set('client_secret', 'secret');

      return this.http
         .post<any>(this.tokenURL, body.toString(), {
            headers: headers,
         })
         .pipe(
            map(jwt => {
               if (jwt && jwt.access_token) {
                  localStorage.setItem('token', JSON.stringify(jwt));
                  this.getUser();
               }
            })
         );
   }

   private getUser() {
      this.getUserInfo().subscribe(res => {
         const isAdmin = res.role === 'Administrator';
         this.isAdminSubject.next(isAdmin);
         localStorage.setItem('user', JSON.stringify(res));
      });
   }

   restore() {
      if (this.isAuthenticated()) {
         this.getUser();
      }
   }

   isAuthenticated(): boolean {
      const token = localStorage.getItem('token');
      const isAuthenticated = !this.jwtHelper.isTokenExpired(token);
      this.isAuthenticatedSubject.next(isAuthenticated);
      return isAuthenticated;
   }

   getToken() {
      return this.jwtHelper.tokenGetter();
   }

   logout() {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      this.isAuthenticated();
      this.isAdminSubject.next(false);
   }
}
