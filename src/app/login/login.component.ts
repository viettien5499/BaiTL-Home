import { BaseComponent } from './../lib/base-component';
import { Component, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent implements OnInit {
  public registerForm: FormGroup;
  public loginForm: FormGroup;
  constructor(injector: Injector) { 
    super(injector);
  }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      email: new FormControl('', [Validators.required,Validators.email]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
      ])
    });
    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
      ]),
      remember: new FormControl(false, []),
    });
  }
  onSubmitLogin(value: any) { 

  }
  onSubmitRegister(value: any) { 

    this._api.post('/api/khachhang/create-item', {Email:value.email, Password:value.password} ).takeUntil(this.unsubscribe).subscribe(res => {
     alert('Tạo thành công');
      }, err => { });      

  }


}