import { Component } from '@angular/core';
import { IProduct } from 'src/app/models/product';
import { environment } from 'src/environments/environment';
import { ProductService } from '../../product.service';
import { ProductParams } from 'src/app/models/productParams';
import { IPagination } from 'src/app/models/pagination';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent {
  products: IProduct[] = [];
  imageUrl = environment.imageUrl;
  selectedId : number|null = null;
  productParams:ProductParams={
    pageIndex :1,
    pageSize :6,
    brandId:0,
    typeId:0,
    sort:'name',
    search:''
  };
  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.updateProductsList();
  }
  updateId(id:number|null){
    this.selectedId = id
  }
  updateProductsList(){
    this.productService.GetAll(this.productParams).subscribe({
      next:
      (response:IPagination|null) =>{
        this.products = response!.data;
        this.productParams.pageIndex = response!.pageIndex;
        this.productParams.pageSize = response!.pageSize;
      },
      error:
        error => {
          console.log(error)
        }
    }
    );
  }
  
  updateData(modified:boolean){
    if(modified){
      this.updateProductsList();
    }
  }
  deleteProduct(id: number) { 
    this.productService.Delete(id).subscribe({
      next:
        (response: boolean | any) => {
          this.updateProductsList();
        },
      error:
        error => {
          console.log(error)
        }
    }
    );
  }
}
