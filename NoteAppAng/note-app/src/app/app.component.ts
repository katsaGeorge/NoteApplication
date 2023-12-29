import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { UserRegisterComponent } from './user-register/user-register.component';
import {UserLoginComponent} from "./user-login/user-login.component";
import {AboutInfoComponent} from "./about-info/about-info.component";
import {BasicLayoutComponent} from "./basic-layout/basic-layout.component";
import {UserNotesComponent} from "./user-notes/user-notes.component";
import {CreateNoteComponent} from "./user-notes/create-note/create-note.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet,],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'note-app';
}
