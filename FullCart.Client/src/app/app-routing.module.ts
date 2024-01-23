import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { authGuard } from './core/guards/auth.guard';

const routes: Routes = [
  {path:'account',loadChildren:()=> import('./account/account.module').then(mod =>mod.AccountModule)},
  {path:'',canActivate:[authGuard],loadChildren:()=> import('./brand/brand.module').then(mod =>mod.BrandModule)},
  {path:'category',canActivate:[authGuard],loadChildren:()=> import('./category/category.module').then(mod =>mod.CategoryModule)},
  {path:'product',canActivate:[authGuard],loadChildren:()=> import('./product/product.module').then(mod =>mod.ProductModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
