import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterOutlet} from "@angular/router";
import {AboutInfoComponent} from "../about-info/about-info.component";
import {UserRegisterComponent} from "../user-register/user-register.component";
import {User} from "../models/user";

@Component({
  selector: 'app-basic-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, AboutInfoComponent, UserRegisterComponent],
  templateUrl: './basic-layout.component.html',
  styleUrl: './basic-layout.component.css'
})
export class BasicLayoutComponent {



}
