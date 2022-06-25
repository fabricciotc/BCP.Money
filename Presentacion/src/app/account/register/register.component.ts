import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';
import { IUserInfo } from '../user-info';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private fb: FormBuilder,
      private accountService: AccountService,
      private router: Router) { }
  formGroup!: FormGroup;

  ngOnInit() {
    this.formGroup = this.fb.group({
          nombreCompleto:'',
          email: '',
          password: '',
          username:''
      });
  }

 public registrarse() {
      let userInfo: IUserInfo = Object.assign({}, this.formGroup.value);
      this.accountService.create(userInfo).subscribe(token => this.recibirToken(token),
          error => this.manejarError(error));
  }

private recibirToken(token:any) {
      localStorage.setItem('token', token.token);
      localStorage.setItem('tokenExpiration', token.expiration);
      this.router.navigate([""]);
  }

  manejarError(error:any) {
      if (error && error.error) {
          alert(error.error[""]);
      }
  }
}
