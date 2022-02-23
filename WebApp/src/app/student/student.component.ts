import { SelectionModel } from '@angular/cdk/collections';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Student } from '../model/Student';
import { CommonService } from '../Service/common.service';
import { ServerHttpService } from '../Service/server-http.service';

export interface DialogData {
  animal: string;
  name: string;
}
@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.scss'],
})
export class StudentComponent implements OnInit {
  public Students: Student[] = [];
  public displayedColumns: any;
  selection = new SelectionModel<Student>(true, []);
  constructor(
    private common: CommonService,
    private serverHttp: ServerHttpService,
    public dialog: MatDialog
  ) {}
  dataSource = new MatTableDataSource<Student>(this.Students);
  public IdRowSelected:any;
  ngOnInit(): void {
    this.displayedColumns = [
      'select',
      'id',
      'code',
      'gender',
      'firstName',
      'lastName',
      'dob',
      'email',
      'phone',
    ];
    this.serverHttp.getStudent().subscribe((data) => {
      this.Students = data;
    });
  }
  NewOpenPopup(): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '50%';

    dialogConfig.data = {};
    this.dialog.open(DialogOverviewExampleDialog, dialogConfig);
  }
  masterToggle() {
    debugger;
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.data.forEach((row: any) => this.selection.select(row));
  }
  isAllSelected() {
    debugger;
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    // this.IdRowSelected.push(this.selection.selected[numSelected].id);
    return numSelected === numRows;
  }
  GetSeletedID(){
    debugger;
    this.IdRowSelected = this.selection.selected.map(x => x.id);
  }
}

@Component({
  selector: 'Student-PopUpNew',
  styleUrls: ['./Student-PopUpNew.scss'],
  templateUrl: 'Student-PopUpNew.html',
})
export class DialogOverviewExampleDialog {
  public StudentInfor = new FormGroup({
    code: new FormControl(''),
    gender: new FormControl(''),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    DoB: new FormControl(''),
    Email: new FormControl(''),
    Phone: new FormControl(''),
  });
  constructor(
    public dialogRef: MatDialogRef<DialogOverviewExampleDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private common: CommonService,
    private serverHttp: ServerHttpService,
    public dialog: MatDialog
  ) {}
  onClickCreate(): void {
    var newStudent = {};
    for (const control in this.StudentInfor.controls) {
      if (control) {
        debugger;
        newStudent[control] = this.StudentInfor.controls[control].value;
      }
    }
    console.log(newStudent);
    this.serverHttp.addStudent(newStudent as Student).subscribe((data) => {
      console.log(data);
    });
    this.dialogRef.close();
  }
  saveAndContinue(): void {
    var newStudent = {};
    this.serverHttp.addStudent(newStudent as Student).subscribe((data) => {
      console.log(data);
    });
  }
  onClose(): void {
    this.dialogRef.close();
  }
}
