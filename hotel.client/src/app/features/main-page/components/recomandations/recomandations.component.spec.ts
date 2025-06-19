import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecomandationsComponent } from './recomandations.component';

describe('RecomandationsComponent', () => {
  let component: RecomandationsComponent;
  let fixture: ComponentFixture<RecomandationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RecomandationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecomandationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
