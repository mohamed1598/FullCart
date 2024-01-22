import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../../category.service';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent {
  categoryForm: FormGroup;
  selectedFile: File | null = null; // Track the selected file

  constructor(private fb: FormBuilder, private categoryService:CategoryService) {
    this.categoryForm = this.fb.group({
      name: ['', Validators.required],
      picture: [null, Validators.required]
    });
  }

  onSubmit(): void {
    let formData = new FormData();
    formData.append('name', this.categoryForm.get('name')!.value);
    if (this.selectedFile) {
      formData.append('picture', this.selectedFile, this.selectedFile.name);
    }
    this.categoryService.create(formData).subscribe(response => {
      console.log('category inserted successfully:', response);
    });
  }

  onFileSelected(event: any): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.selectedFile = fileInput.files[0] as File;
    }
  }
}
