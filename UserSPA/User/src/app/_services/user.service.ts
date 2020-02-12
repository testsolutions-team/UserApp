import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable()
export class UserService {

    baseUrl = environment.apiUrl ;
  
    constructor(private http: HttpClient ) {  }
  
    getUsers(): Observable<User[]> {
      const url = this.baseUrl + 'users';
      return this.http.get<User[]>(url);
    }

    getUser(id): Observable<User> {
      return this.http.get<User>(this.baseUrl + 'users/' + id);
    }
  
    createUser(user: User): Observable<User> {
      const url = this.baseUrl + 'users/CreateUser/';
      return this.http.post<User>(url, user);
    }
    
    updateUser(user: User): Observable<User> {
      return this.http.put<User>(this.baseUrl + 'users/UpdateUser/', user );
    }
    
    deleteUserById(userId: number): Observable<number> {
      return this.http.put<number>(this.baseUrl + 'users/DeleteUser/' + userId);
    }




}
