import {Component, EventEmitter, Output} from '@angular/core';
import {NgOptimizedImage} from "@angular/common";

@Component({
  selector: 'app-navbar',
  standalone: true,
    imports: [
        NgOptimizedImage
    ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  @Output('addButtonClicked') addButtonClicked: EventEmitter<void> = new EventEmitter();

  handleButtonClick() {
    this.addButtonClicked.emit();
  }
}
