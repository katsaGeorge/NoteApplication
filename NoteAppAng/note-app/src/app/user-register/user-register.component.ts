import  {Component, EventEmitter, inject, Inject, OnInit, Output} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {
  AbstractControl,
  Form,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from "@angular/forms";
import {confirmPasswordValidator} from "../../shared/confirm-password.validator";
import {AppService} from "../app.service";
import {User} from "../models/user";
import {NavigationExtras, Router, RouterOutlet} from '@angular/router';
import {async, Observable} from "rxjs";
import {error} from "@angular/compiler-cli/src/transformers/util";

@Component({
  selector: 'app-user-register',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatInputModule, MatSelectModule, ReactiveFormsModule, RouterOutlet,
  ],
  templateUrl: './user-register.component.html',
  styleUrl: './user-register.component.css'
})
export class UserRegisterComponent implements OnInit{


  errorInRegister :boolean = false;
  errormessage:any;

  username: string = '';
  password: string = '';
  confPass: string = '';
  registerForm: FormGroup;
  createUser: boolean = false;

  constructor(private fb: FormBuilder, private router: Router, private service : AppService = inject(AppService)) {
    this.registerForm = new FormGroup({
        username: new FormControl<string>('', [Validators.required, Validators.minLength(2)]),
        password: new FormControl<string>('', [Validators.required, Validators.minLength(8)]),
        confPass: new FormControl<string>('')

      },
      {validators: confirmPasswordValidator}
    );
  }

ngOnInit() {

}

   async register() {
    console.log(this.registerForm.value);
    this.errorInRegister = false;
     const  newUser: any = {
      username: this.registerForm.controls['username'].value,
      password: this.registerForm.controls['password'].value,
      confPassword: this.registerForm.controls['confPass'].value
    }


   /*this.service.createUser(newUser).subscribe((res) =>{
      console.log(res);
      const  user = newUser as User;

      if(user){
        this.service.makeLoginSession(user)
        this.router.navigate(['/user-notes']);
      }



    }, error => {
      this.errorInRegister = true;
      this.errormessage  = error.error.errors.message
      console.log(this.errormessage);
    })*/



     this.service.createUser(newUser).subscribe({
      complete: () => this.router.navigate(['user-notes']),
      next: (res) => {
        const loggedInUser = res as User
        this.service.makeLoginSession(loggedInUser)
      },
      error: (error:any) => {
        console.log(error)
        this.errorInRegister = true
        this.errormessage = error.error.errors.message

      }})
   }

}









