import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../product.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent {
  @Input() id: number | null = null;
  productForm: FormGroup;
  selectedFile: File | null = null; // Track the selected file

  constructor(private fb: FormBuilder, private productService: ProductService) {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', Validators.required],
      picture: [null, Validators.required]
    });
  }

  onSubmit(): void {
    let formData = new FormData();
    console.log(this.productForm.value);
    formData.append('name', this.productForm.get('name')!.value);
    formData.append('description', this.productForm.get('description')!.value);
    formData.append('price', this.productForm.get('price')!.value);
    formData.append('brandId', '5');
    formData.append('categoryId', '6');
    if (this.selectedFile) {
      formData.append('picture', this.selectedFile, this.selectedFile.name);
    }
    if (this.id == null) {
      this.productService.create(formData).subscribe(response => {
        console.log('Product inserted successfully:', response);
      });
    }else{
      formData.append('id', this.id.toString());
      this.productService.Update(formData).subscribe(response => {
        console.log('Product updated successfully:', response);
      });
    }

  }

  onFileSelected(event: any): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.selectedFile = fileInput.files[0] as File;
    }
  }
}
