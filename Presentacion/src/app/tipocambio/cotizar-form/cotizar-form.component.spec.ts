import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CotizarFormComponent } from './cotizar-form.component';

describe('CotizarFormComponent', () => {
  let component: CotizarFormComponent;
  let fixture: ComponentFixture<CotizarFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CotizarFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CotizarFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
