import { Component } from '@angular/core';

@Component({
  selector: 'app-heroi-modal',
  standalone: true,
  imports: [],
  templateUrl: './heroi-modal.component.html',
  styleUrl: './heroi-modal.component.scss'
})
export class HeroiModalComponent {
  @Input() showModal = false;
  @Input() heroData: any = null; // null = novo, objeto = edição
  @Output() closeModal = new EventEmitter<void>();
  @Output() saveHero = new EventEmitter<any>();

  heroForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.heroForm = this.fb.group({
      id: [''],
      name: ['', Validators.required],
      alterEgo: ['', Validators.required],
      description: [''],
      powers: ['', Validators.required],
    });
  }

  ngOnChanges() {
    if (this.heroData) {
      this.heroForm.patchValue(this.heroData);
    } else {
      this.heroForm.reset();
    }
  }

  onSubmit() {
    if (this.heroForm.valid) {
      this.saveHero.emit(this.heroForm.value);
      this.closeModal.emit();
    }
  }
}
