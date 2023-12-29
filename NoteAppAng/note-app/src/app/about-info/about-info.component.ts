import {Component, Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {Routes} from "@angular/router";
import {User} from "../models/user";

@Component({
  selector: 'app-about-info',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './about-info.component.html',
  styleUrl: './about-info.component.css'
})
export class AboutInfoComponent {
  @Input() user : User | undefined;
}
