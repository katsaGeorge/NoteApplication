import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {Note} from "../models/note";
import {MatExpansionModule} from "@angular/material/expansion";
import {CreateNoteComponent} from "./create-note/create-note.component";
import {MatDialog} from "@angular/material/dialog";
import {EditNoteComponent} from "./edit-note/edit-note.component";
import {ConfirmDialogComponent} from "./confirm-dialog/confirm-dialog.component";
import {ActivatedRoute, Router} from "@angular/router";
import {AppService} from "../app.service";
import {User} from "../models/user";
import {error} from "@angular/compiler-cli/src/transformers/util";

@Component({
  selector: 'app-user-notes',
  standalone: true,
  imports: [CommonModule, MatExpansionModule],
  templateUrl: './user-notes.component.html',
  styleUrl: './user-notes.component.css'
})
export class UserNotesComponent implements OnInit {
    panelOpenState = false;
    notes: Note[] = [];
    index: any;
    id :any;
    username: any;
    constructor(public dialog: MatDialog, private route: ActivatedRoute, private service: AppService, private router: Router) {
    }

    ngOnInit() {
      /*this.route.queryParams.subscribe(params=>{
        console.log(params);
        this.id = params['id'];
        this.username = params['username'];
        console.log(this.id);*/


          console.log(this.service.session?.id);
          this.id = this.service.session?.id
          this.username = this.service.session?.username
          this.loadpage();

    }

     async loadpage() {
      this.service.getAllNotes(this.id).subscribe(res =>{
        this.notes  = res;
      },error =>{
        console.log(error)
        }
      )}

    openCreateModal() {
        const dialogRef = this.dialog.open(CreateNoteComponent, {
            width: '700px',
            autoFocus: false
        })
    }

    openEditModal(note: any) {
        const dialogRef = this.dialog.open(EditNoteComponent, {
            width: '700px',
            autoFocus: false,
            data: note
        })
    }
    async deleteModal( note:Note ){
        const dialogRef = this.dialog.open(ConfirmDialogComponent,{
            width:'500px',
            autoFocus:false,
            data: note
        })

    }

    logOutFunc(){
      this.service.logout();
    }
}
