import {Component, EventEmitter, Input, Output, SimpleChanges} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';
import {Superhero, Superpoder} from '../../../core/types/hero';

@Component({
  selector: 'app-hero-modal',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './hero-modal.component.html',
  styleUrl: './hero-modal.component.scss'
})
export class HeroModalComponent {
  @Input() showModal = true;
  @Input() heroData: Superhero | null = null;
  @Output() closeModal = new EventEmitter<void>();
  @Output() saveHero = new EventEmitter<any>();

  powerInput = new FormControl('');
  allPowers: Superpoder[] = [
    { id: 1, nome: 'Flight', descricao: 'Fly through the sky' },
    { id: 2, nome: 'Invisibility', descricao: 'Become invisible' },
    { id: 3, nome: 'Telepathy', descricao: 'Read minds' },
    { id: 4, nome: 'Super Strength', descricao: 'Lift heavy objects' },
    { id: 5, nome: 'Speed', descricao: 'Move very fast' },
    { id: 6, nome: 'Healing', descricao: 'Heal rapidly' },
    { id: 7, nome: 'Laser Vision', descricao: 'Shoot lasers from eyes' },
    { id: 8, nome: 'Teleportation', descricao: 'Instantly travel' },
  ];
  filteredPowers: Superpoder[] = [];
  selectedPowers: Superpoder[] = [];
  showSuggestions = false;

  heroForm: FormGroup;

  constructor(private readonly fb: FormBuilder) {
    this.heroForm = this.fb.group({
      nome: ['', Validators.required],
      nomeHeroi: ['', Validators.required],
      heroisSuperPoderes: [[], Validators.required],
      peso: ['', Validators.required],
      altura: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.powerInput.valueChanges.subscribe(value => {
      if (value) {
        const input = value.toLowerCase();
        this.filteredPowers = this.allPowers.filter(p =>
          p.nome.toLowerCase().includes(input) && !this.selectedPowers.includes(p)
        );
        this.showSuggestions = true;
      } else {
        this.filteredPowers = this.allPowers.filter(p => !this.selectedPowers.includes(p));
        this.showSuggestions = true;
      }
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['heroData']) {
      if (this.heroData) {
        this.heroForm.patchValue(this.heroData);
        this.selectedPowers = this.heroData.heroisSuperPoderes || [];
      } else {
        this.heroForm.reset();
        this.selectedPowers = [];
      }
    }
  }

  handleCloseModal() {
    this.closeModal.emit();
  }

  onSubmit() {
    if (this.heroForm.valid) {
      this.heroForm.patchValue({ heroisSuperPoderes: this.selectedPowers });
      this.saveHero.emit(this.heroForm.value);
      this.closeModal.emit();
    }
  }

  onFocusInput() {
    this.filteredPowers = this.allPowers.filter(p => !this.selectedPowers.includes(p));
    this.showSuggestions = true;
  }

  onBlurInput() {
    setTimeout(() => {
      this.showSuggestions = false;
    }, 100);
  }

  addPowerFromInput(event: KeyboardEvent): void {
    const input = (event.target as HTMLInputElement);
    const value = input.value.trim();

    if ((event.key === 'Enter' || event.key === ',') && value) {
      event.preventDefault();
      const matchedPower = this.allPowers.find(p => p.nome.toLowerCase() === value.toLowerCase());
      if (matchedPower) {
        this.addPower(matchedPower);
      }
      input.value = '';
      this.powerInput.setValue('');
    }
  }

  addPower(power: Superpoder): void {
    if (!this.selectedPowers.some(p => p.id === power.id)) {
      this.selectedPowers.push(power);
      this.heroForm.patchValue({ heroisSuperPoderes: this.selectedPowers });
    }
  }

  removePower(power: Superpoder): void {
    this.selectedPowers = this.selectedPowers.filter(p => p.id !== power.id);
    this.heroForm.patchValue({ heroisSuperPoderes: this.selectedPowers });
  }

  selectFromDropdown(power: Superpoder): void {
    this.addPower(power);
    this.filteredPowers = [];
    this.powerInput.setValue('');
    this.showSuggestions = false;
  }
}
