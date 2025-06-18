import {Component, EventEmitter, Output} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {NgIf, NgOptimizedImage} from '@angular/common';

@Component({
  selector: 'app-filter',
  standalone: true,
  imports: [
    FormsModule,
    NgOptimizedImage,
    NgIf
  ],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.scss'
})
export class FilterComponent {
  filterId: string = '';
  showClearButton: boolean = false;

  @Output() searchById = new EventEmitter<number>();
  @Output() clearFilter = new EventEmitter<void>();

  onSearch() {
    const id = Number(this.filterId);
    if (!isNaN(id)) {
      this.searchById.emit(id);
    }
    this.showClearButton = true;
  }

  onClear() {
    this.filterId = '';
    this.clearFilter.emit();
    this.showClearButton = false;
  }
}
