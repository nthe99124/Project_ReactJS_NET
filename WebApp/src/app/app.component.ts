
import { Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'ProjectAngular';
  @ViewChild('naviSide') naviSide?: MatSidenav;
  public isOpen = false;

public openLeftSide() {
  this.isOpen = !this.isOpen;
  this.naviSide?.toggle();
}
public closeLeftSide() {
  this.isOpen = false;
}
}