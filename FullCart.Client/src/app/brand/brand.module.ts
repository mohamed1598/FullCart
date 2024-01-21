import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BrandRoutingModule } from './brand-routing.module';
import { BrandListComponent } from './Components/brand-list/brand-list.component';
import { BrandFormComponent } from './Components/brand-form/brand-form.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    BrandListComponent,
    BrandFormComponent
  ],
  imports: [
    CommonModule,
    BrandRoutingModule,
    SharedModule
  ]
})
export class BrandModule { }
