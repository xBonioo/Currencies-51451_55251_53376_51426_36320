import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  private apiUrl = 'https://localhost:7050/api/';

  constructor(private http: HttpClient) {}
  login(login: string, password: string) {
    
    let obj = {
      Username: login,
      Password: password
    }
    return this.http.post(`${this.apiUrl}user/signin`, obj);
  }
}
//https://localhost:7050/api/currency?PageNumber=1&PageSize=5