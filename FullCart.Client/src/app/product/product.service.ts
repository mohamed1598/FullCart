import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  GetAll(){
     return this.http.get(this.baseUrl + 'Products');
  }
  create(formData:FormData){
    return this.http.post(`${this.baseUrl}Products`, formData);
  }
  Update(formData:FormData){
    return this.http.put(`${this.baseUrl}Products`, formData);
  }
  Delete(id:number){
    return this.http.delete(`${this.baseUrl}Products?id=${id}`);
  }
  uploadFile(formData:FormData){
    return this.http.post(`${this.baseUrl}Products/Import`, formData);
  }
}
