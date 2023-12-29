import {Component, Inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Note} from "../../models/note";
import {MAT_DIALOG_DATA, MatDialogModule, MatDialogRef} from "@angular/material/dialog";
import {AppService} from "../../app.service";
import {Router} from "@angular/router";
import {error} from "@angular/compiler-cli/src/transformers/util";

@Component({
  selector: 'app-create-note',
  standalone: true,
    imports: [CommonModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule, MatDialogModule],
  templateUrl: './create-note.component.html',
  styleUrl: './create-note.component.css'
})
export class CreateNoteComponent implements OnInit{
createForm : FormGroup;
id: any

  constructor(private fb: FormBuilder,
              public dialogRef: MatDialogRef<CreateNoteComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private service: AppService,
              private router: Router) {
    this.createForm = this.fb.group({
      subject: ['',Validators.maxLength(128)],
      text: ['', Validators.required]
    })
  }
  ngOnInit() {
    this.id = this.service.session?.id
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
   create(){
    if(this.createForm.invalid){
      return;
    }
    const newNote: any = {
      subject : this.createForm.controls['subject'].value,
      text : this.createForm.controls['text'].value,
      date: new Date(),
      userId: this.id
    }

    this.service.createNote(newNote).subscribe(res =>{
      this.dialogRef.close();
      //this.router.navigate(['user-notes'])
      window.location.reload();
    }, error=>{
      console.log(error);
    })
 }
}
