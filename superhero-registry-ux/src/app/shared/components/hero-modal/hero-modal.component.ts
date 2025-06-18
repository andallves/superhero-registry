import {Component, EventEmitter, Input, Output, SimpleChanges} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';
import {Superhero, Superpoder} from '../../../core/types/hero';
import {LoadingComponent} from '../loading/loading.component';
import {SuperpowerService} from '../../../core/services/superpower.service';
import {Response} from '../../../core/types/response';

@Component({
  selector: 'app-hero-modal',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
    NgForOf,
    LoadingComponent
  ],
  templateUrl: './hero-modal.component.html',
  styleUrl: './hero-modal.component.scss'
})
export class HeroModalComponent {
  @Input() showModal = false;
  @Input() heroData: Superhero | null = null;
  @Input() isLoading: boolean = false;
  @Input() allPowers: Superpoder[] = [];
  @Output() closeModal = new EventEmitter<void>();
  @Output() saveHero = new EventEmitter<Superhero>();
  @Output() updateHero = new EventEmitter<Superhero>();

  powerInput = new FormControl('');
  filteredPowers: Superpoder[] = [];
  selectedPowers: Superpoder[] = [];
  showSuggestions = false;

  heroForm: FormGroup;

  constructor(private readonly fb: FormBuilder, private readonly superpowerService: SuperpowerService) {
    this.heroForm = this.fb.group({
      id: [''],
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
          p.nome.toLowerCase().includes(input) && !this.selectedPowers.some(sp => sp.id === p.id)
        );
        this.showSuggestions = true;
      } else {
        this.filteredPowers = this.allPowers.filter(p => !this.selectedPowers.includes(p));
        this.showSuggestions = true;
      }
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['heroData'] && this.heroData) {
      this.heroForm.patchValue(this.heroData);
      const initialSelectedPowers = (this.heroData.heroisSuperPoderes || [])
        .map(hsp => this.allPowers.find(p => p.id === hsp.superPoderId))
        .filter((p): p is Superpoder => !!p);

      // Garante que os poderes iniciais sejam únicos
      this.selectedPowers = Array.from(new Set(initialSelectedPowers.map(p => p.id)))
        .map(id => initialSelectedPowers.find(p => p.id === id)!)
        .filter((p): p is Superpoder => !!p); // Adicionado para garantir que o tipo seja Superpoder

    } else if (changes['heroData'] && !this.heroData) { // Adicionando condição para quando heroData se torna null
      this.heroForm.reset();
      this.selectedPowers = [];
    }
  }

  handleCloseModal() {
    this.closeModal.emit();
  }

  onSubmit() {
    this.isLoading = true;
    if (this.heroForm.valid) {
      this.heroForm.patchValue({
        heroisSuperPoderes: this.selectedPowers.map(power => ({
          superPoderId: power.id
        }))
      });
      if (this.heroData) {
        this.updateHero.emit(this.heroForm.value);
      } else {
        this.saveHero.emit(this.heroForm.value);
      }
    }
  }

  onFocusInput() {
    this.filteredPowers = this.allPowers.filter(p => !this.selectedPowers.some(sp => sp.id === p.id));
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
