import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroiCardComponent } from './heroi-card.component';

describe('HeroiCardComponent', () => {
  let component: HeroiCardComponent;
  let fixture: ComponentFixture<HeroiCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HeroiCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HeroiCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
