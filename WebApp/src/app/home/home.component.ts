import { Component, OnInit } from '@angular/core';
import { CommonService } from '../Service/common.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  public name = 'Tiến Thế';
  public html = '';
  public age = 24;
  public position = 'Nhân viên Dev';
  public depart = 'Phát triển ứng dụng';
  public listColor = ['red', 'blue', 'green'];
  constructor(private commonService: CommonService) {
    this.age = commonService.age;
  }
  ngOnInit(): void {}
  public onSubmit() {
    alert('Đã nhập thành công');
    this.html = '<h1>Hello World</h1>';
  }
  public tangTuoi() {
    this.commonService.age++;
    this.age = this.commonService.age;
  }
}
