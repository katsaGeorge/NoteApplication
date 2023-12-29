import {Component, EventEmitter, Inject, Output} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from "@angular/material/dialog";
import {AppService} from "../../app.service";

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogTitle, MatDialogActions, MatDialogContent],
  templateUrl: './confirm-dialog.component.html',
  styleUrl: './confirm-dialog.component.css'
})
export class ConfirmDialogComponent {

  @Output() isConfirmed: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(public dialogRef: MatDialogRef<ConfirmDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data:any,
              private service : AppService) {
  }


  async confirm(accept: boolean){
    if(accept) {
      this.service.deleteNote(this.data.id).subscribe(res => {
        this.dialogRef.close();

      }, error => {
        console.log(error)
      });
    }else(!accept)
    {
      this.dialogRef.close();
    }
    if(accept) window.location.reload();

    //this.isConfirmed.emit(accept);

}
}
