import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BrandService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  GetAll(){
     return this.http.get(this.baseUrl + 'Brands');
  }
  create(formData:FormData){
    return this.http.post(`${this.baseUrl}Brands`, formData);
  }
}
