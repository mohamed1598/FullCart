import { Component } from '@angular/core';
import { IBrand } from 'src/app/models/brand';
import { BrandService } from '../../brand.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-brand-list',
  templateUrl: './brand-list.component.html',
  styleUrls: ['./brand-list.component.css']
})
export class BrandListComponent {
  brands: IBrand[] = [];
  imageUrl = environment.imageUrl;
  selectedId : number|null = null;
  constructor(private brandService: BrandService) { }

  ngOnInit(): void {
    this.updateBrandsList();
  }
  updateId(id:number|null){
    this.selectedId = id
  }
  updateData(modified:boolean){
    if(modified){
      this.updateBrandsList();
    }
  }
  updateBrandsList(){
    this.brandService.GetAll().subscribe({
      next:
        (response: IBrand[] | any) => {
          this.brands = response!;
        },
      error:
        error => {
          console.log(error)
        }
    }
    );
  }
  deleteBrand(id: number) { 
    this.brandService.Delete(id).subscribe({
      next:
        (response: boolean | any) => {
          this.updateBrandsList();
        },
      error:
        error => {
          console.log(error)
        }
    }
    );
  }

}
