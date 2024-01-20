import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm:FormGroup = new FormGroup({});
  returnUrl='/shop'
  constructor(private accountService:AccountService,private router:Router,private route:ActivatedRoute){
  }
  ngOnInit(): void {    
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    this.createLoginForm();
  }
  createLoginForm(){
    this.loginForm = new FormGroup({
      email: new FormControl('',[Validators.required,Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
      password: new FormControl('',Validators.required)
    });
  }
  onSubmit(){
    this.accountService.login(this.loginForm.value).subscribe({
      next:()=>{
        this.router.navigateByUrl(this.returnUrl);
      },error: error=>{
        console.log(error);
        
      }
    })
  }
}
