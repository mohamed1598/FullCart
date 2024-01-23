import { Component } from '@angular/core';
import { IProduct } from 'src/app/models/product';
import { environment } from 'src/environments/environment';
import { ProductService } from '../../product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent {
  products: IProduct[] = [];
  imageUrl = environment.imageUrl;
  selectedId : number|null = null;
  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.updateProductsList();
  }
  updateId(id:number|null){
    this.selectedId = id
  }
  updateProductsList(){
    this.productService.GetAll().subscribe({
      next:
        (response: IProduct[] | any) => {
          this.products = response!;
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
