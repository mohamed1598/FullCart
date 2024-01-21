import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from 'src/app/models/user';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  currentUser$:Observable<IUser | null>;
  constructor(private accountService:AccountService){
    this.currentUser$ = this.accountService.currentUser$;
  }

  ngOnInit(){

  }

  logout(){
    this.accountService.logout();
  }
}
