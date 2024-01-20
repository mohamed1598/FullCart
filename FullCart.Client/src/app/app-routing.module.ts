import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';

const routes: Routes = [
  {path:"",component:LoginComponent},
  {path:'account',loadChildren:()=> import('./account/account.module').then(mod =>mod.AccountModule), data:{breadcrumb: {skip:true}}}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
