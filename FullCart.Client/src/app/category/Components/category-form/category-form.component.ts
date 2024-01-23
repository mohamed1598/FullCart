import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../../category.service';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent {
  @Output() dataModified: EventEmitter<boolean> = new EventEmitter<boolean>();
  @ViewChild('closeModal') closeModal: ElementRef|any;
  @Input() id: number | null = null;
  categoryForm: FormGroup;
  selectedFile: File | null = null; // Track the selected file

  constructor(private fb: FormBuilder, private categoryService: CategoryService) {
    this.categoryForm = this.fb.group({
      name: ['', Validators.required],
      picture: [null, Validators.required]
    });
  }

  onSubmit(): void {
    let formData = new FormData();
    console.log(this.categoryForm.value);
    formData.append('name', this.categoryForm.get('name')!.value);
    if (this.selectedFile) {
      formData.append('picture', this.selectedFile, this.selectedFile.name);
    }
    if (this.id == null) {
      this.categoryService.create(formData).subscribe(response => {
        this.displayDataModified();
        console.log('Category inserted successfully:', response);
      });
    }else{
      formData.append('id', this.id.toString());
      this.categoryService.Update(formData).subscribe(response => {
        this.displayDataModified();
        console.log('Category updated successfully:', response);
      });
    }

  }
  displayDataModified(){
    this.closeModal.nativeElement.click();
    this.dataModified.emit(true);
  }
  onFileSelected(event: any): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.selectedFile = fileInput.files[0] as File;
    }
  }
}
