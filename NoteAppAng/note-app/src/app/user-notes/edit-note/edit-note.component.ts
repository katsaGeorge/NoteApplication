import {Component, Inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {TextFieldModule} from "@angular/cdk/text-field";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Note} from "../../models/note";
import {AppService} from "../../app.service";

@Component({
  selector: 'app-edit-note',
  standalone: true,
    imports: [CommonModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule, TextFieldModule],
  templateUrl: './edit-note.component.html',
  styleUrl: './edit-note.component.css'
})
export class EditNoteComponent implements  OnInit{
  editForm : FormGroup;


  constructor(public dialogRef: MatDialogRef<EditNoteComponent>,
              @Inject(MAT_DIALOG_DATA) public data:any,
              private fb: FormBuilder,
              private service:AppService) {

    this.editForm = this.fb.group({
      subject: ['', Validators.maxLength(128)],
      text:['', Validators.required]
    })
  }

  ngOnInit() {
    this.fillForm();
  }
  onNoClick(): void {
    this.dialogRef.close();
  }

  fillForm(): void{
    this.editForm.controls['subject'].setValue(this.data.subject)
    this.editForm.controls['text'].setValue(this.data.text)
  }

  edit(): void{
    if (this.editForm.invalid){
      return;
    }
    const editNote : Note ={
      subject: this.editForm.controls['subject'].value,
      text: this.editForm.controls['text'].value
    }

    this.service.editNote(this.data.id,editNote).subscribe(res =>{

    }, error=>{
      console.log(error);
    })
    this.dialogRef.close();
    window.location.reload();
  }
}
