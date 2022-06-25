import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TipocambioComponent } from './tipocambio.component';

describe('TipocambioComponent', () => {
  let component: TipocambioComponent;
  let fixture: ComponentFixture<TipocambioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TipocambioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TipocambioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
