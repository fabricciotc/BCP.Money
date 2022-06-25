import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TipocambioFormComponent } from './tipocambio-form.component';

describe('TipocambioFormComponent', () => {
  let component: TipocambioFormComponent;
  let fixture: ComponentFixture<TipocambioFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TipocambioFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TipocambioFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
