import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs';
import { IPagination } from '../models/pagination';
import { ProductParams } from '../models/productParams';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  GetAll(shopParams:ProductParams){
    let params = new HttpParams();
    if(shopParams.brandId !== 0){
      params = params.append('brandId',shopParams.brandId.toString());
    }
    if(shopParams.typeId !== 0){
      params = params.append('categoryId',shopParams.typeId.toString());
    }
    if(shopParams.sort){
      params = params.append('sort',shopParams.sort);
    }
    if(shopParams.pageIndex){
      params = params.append('pageIndex',shopParams.pageIndex);
    }
    if(shopParams.pageSize){
      params = params.append('pageSize',shopParams.pageSize);
    }
    if(shopParams.search != ''){
      params = params.append('search',shopParams.search);

    }
    return this.http.get<IPagination>(this.baseUrl + 'Products',{observe:'response',params:params})
    .pipe(map((response:any) =>{
      return response.body;
    }));
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
