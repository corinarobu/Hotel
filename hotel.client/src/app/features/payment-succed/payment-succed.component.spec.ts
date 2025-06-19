import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentSuccedComponent } from './payment-succed.component';

describe('PaymentSuccedComponent', () => {
  let component: PaymentSuccedComponent;
  let fixture: ComponentFixture<PaymentSuccedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PaymentSuccedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentSuccedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
