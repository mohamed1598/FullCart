import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  GetAll(){
     return this.http.get(this.baseUrl + 'Categories');
  }
  create(formData:FormData){
    return this.http.post(`${this.baseUrl}Categories`, formData);
  }
  Update(formData:FormData){
    return this.http.put(`${this.baseUrl}Categories`, formData);
  }
  Delete(id:number){
    return this.http.delete(`${this.baseUrl}Categories?id=${id}`);
  }
}
