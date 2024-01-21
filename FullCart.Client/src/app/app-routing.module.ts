import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';

const routes: Routes = [
  {path:'account',loadChildren:()=> import('./account/account.module').then(mod =>mod.AccountModule)},
  {path:'',loadChildren:()=> import('./brand/brand.module').then(mod =>mod.BrandModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
