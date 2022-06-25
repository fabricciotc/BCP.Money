import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonedasFormComponent } from './monedas-form.component';

describe('MonedasFormComponent', () => {
  let component: MonedasFormComponent;
  let fixture: ComponentFixture<MonedasFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonedasFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MonedasFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
