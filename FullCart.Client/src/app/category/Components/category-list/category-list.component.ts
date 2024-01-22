import { Component } from '@angular/core';
import { ICategory } from 'src/app/models/category';
import { environment } from 'src/environments/environment';
import { CategoryService } from '../../category.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent {
  selectedId : number|null = null;
  categories: ICategory[] = [];
  imageUrl = environment.imageUrl;
  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.updateCategoriesList();
  }
  updateId(id:number|null){
    this.selectedId = id
  }
  updateCategoriesList(){
    this.categoryService.GetAll().subscribe({
      next:
        (response: ICategory[] | any) => {
          this.categories = response!;
        },
      error:
        error => {
          console.log(error)
        }
    }
    );
  }
  deleteCategory(id: number) {
    this.categoryService.Delete(id).subscribe({
      next:
        (response: boolean | any) => {
          this.updateCategoriesList();
        },
      error:
        error => {
          console.log(error)
        }
    }
    );
   }
}
