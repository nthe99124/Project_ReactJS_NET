import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-reactiveform',
  templateUrl: './reactiveform.component.html',
  styleUrls: ['./reactiveform.component.scss']
})
export class ReactiveformComponent implements OnInit {
  // public name = new FormControl('');  
  public profileForm = new FormGroup({
    fname: new FormControl(''),
    age: new FormControl(''),  
  });
  constructor() { }

  ngOnInit(): void {
  }
  public updateName(){
    // this.name.setValue('Nancy'); 
    // this.profileForm.firstName.setValue('Nancy');
    console.log(this.profileForm.controls['fname'].value);
  }
}
 