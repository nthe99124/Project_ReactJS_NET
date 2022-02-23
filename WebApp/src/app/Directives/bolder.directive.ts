import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[MyBolder]',
})
export class BolderDirective {
  constructor(private element: ElementRef) {
    element.nativeElement.style.fontWeight = 'bold';
  }
}
