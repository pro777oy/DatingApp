import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private http=inject(HttpClient);
  baseURL='https://localhost:5001/api/';

  login(model:any){
    return this.http.post(this.baseURL+'account/login',model)
  }

}
