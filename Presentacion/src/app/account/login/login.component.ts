import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';
import { IUserInfo } from '../user-info';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router) { }
  formGroup!: FormGroup;

  ngOnInit() {
    this.formGroup = this.fb.group({
      fullname: '',
      email: '',
      password: '',
    });
  }

  public loguearse() {
      let userInfo: IUserInfo = Object.assign({}, this.formGroup.value);
      this.accountService.login(userInfo).subscribe(token => this.recibirToken(token),
          error => this.manejarError(error));
  }


  private recibirToken(token:any) {
    localStorage.setItem('token', token.token);
    localStorage.setItem('tokenExpiration', token.expiration);
    this.router.navigate([""]);
  }

  manejarError(error:any) {
      alert("Login Invalido ("+error.status+"): " + error.error);
  }

}
