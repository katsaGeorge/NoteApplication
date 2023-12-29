import { Routes } from '@angular/router';
import {UserLoginComponent} from "./user-login/user-login.component";
import {UserRegisterComponent} from "./user-register/user-register.component";
import {AboutInfoComponent} from "./about-info/about-info.component";
import {UserNotesComponent} from "./user-notes/user-notes.component";

export const routes: Routes = [
  {path:"", component:AboutInfoComponent},
  {path:'user-notes', component:UserNotesComponent},
  {path:'login' , component: UserLoginComponent},
  {path:'register', component: UserRegisterComponent}



];
