import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { map, Observable, take } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  baseUrl = "https://localhost:5001/api/";
  currentUser = signal<User | null>(null);

  login(model: any):Observable <User | void >{
    return this.http.post<User>(this.baseUrl + "account/login", model).pipe(map(user => {
      if(user){
        localStorage.setItem("user",JSON.stringify(user));
        this.currentUser.set(user);
      }
    }));
  }
  logout(){
    localStorage.removeItem("user");
    this.currentUser.set(null);
  }
}