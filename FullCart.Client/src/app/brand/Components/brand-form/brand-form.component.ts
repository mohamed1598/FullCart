import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IBrand } from 'src/app/models/brand';
import { BrandService } from '../../brand.service';

@Component({
  selector: 'app-brand-form',
  templateUrl: './brand-form.component.html',
  styleUrls: ['./brand-form.component.css']
})
export class BrandFormComponent {
  @Output() dataModified: EventEmitter<boolean> = new EventEmitter<boolean>();
  @ViewChild('closeModal') closeModal: ElementRef|any;
  @Input() id: number | null = null;
  brandForm: FormGroup;
  selectedFile: File | null = null; // Track the selected file

  constructor(private fb: FormBuilder, private brandService: BrandService) {
    this.brandForm = this.fb.group({
      name: ['', Validators.required],
      picture: [null, Validators.required]
    });
  }

  onSubmit(): void {
    let formData = new FormData();
    console.log(this.brandForm.value);
    formData.append('name', this.brandForm.get('name')!.value);
    if (this.selectedFile) {
      formData.append('picture', this.selectedFile, this.selectedFile.name);
    }
    if (this.id == null) {
      this.brandService.create(formData).subscribe(response => {
        this.displayDataModified();
        console.log('Brand inserted successfully:', response);
      });
    }else{
      formData.append('id', this.id.toString());
      this.brandService.Update(formData).subscribe(response => {
        this.displayDataModified();
        console.log('Brand updated successfully:', response);
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
