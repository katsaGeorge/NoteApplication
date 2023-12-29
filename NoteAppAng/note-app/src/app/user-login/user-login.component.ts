import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {Form, FormControl, FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {AppService} from "../app.service";
import {User} from "../models/user";
import {NavigationExtras, Router} from "@angular/router";
import {combineLatestAll} from "rxjs";

@Component({
  selector: 'app-user-login',
  standalone: true,
    imports: [CommonModule, FormsModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './user-login.component.html',
  styleUrl: './user-login.component.css'
})
export class UserLoginComponent implements OnInit{
  errorInLoggin :boolean = false;
  errormessage:any;
  username: string  = '';
  password : string = '';

  loginForm: FormGroup

  constructor(private service:AppService, private  router:Router) {
    this.loginForm = new  FormGroup({
      username: new FormControl(''),
      password: new FormControl('')
    })
  }

  ngOnInit() {

  }

  async login(){
    console.log(this.loginForm.value);
    this.errorInLoggin = false;
      const loginUser: any = {
        username: this.loginForm.controls['username'].value,
        password: this.loginForm.controls['password'].value,
    }

    this.service.loginUser(loginUser).subscribe((resUser)=>{
      console.log(resUser);
      const  user = resUser as User;
      this.service.makeLoginSession(user);

      this.router.navigate(['/user-notes']);

    }, error => {
      this.errorInLoggin = true;
      this.errormessage  = error.error.errors.message
      console.log(this.errormessage);
    })
  }
}
