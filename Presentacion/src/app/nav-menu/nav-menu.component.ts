import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {


  constructor(private accountService: AccountService,
  private router: Router) { }

  isExpanded = false;

  collapse() {
  this.isExpanded = false;
  }

  toggle() {
  this.isExpanded = !this.isExpanded;
  }

  public obtenerUniqueName(): string|null {
  let token = localStorage.getItem("token");
  console.log(token+"asd")
  let jwtData = token?.split('.')[1];
  console.log(jwtData)
  if(jwtData!=null){

    let decodedJwtJsonData = window.atob(jwtData);
    let decodedJwtData = JSON.parse(decodedJwtJsonData);
    console.log(decodedJwtData)
    return decodedJwtData.nameid;
  }
    return null;
  }

  logout() {
    this.accountService.logout();
    this.router.navigate(['/']);
  }

  estaLogueado() {
      return this.accountService.estaLogueado();
  }

  ngOnInit(): void {
  }

}
