import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HotelFacilitiesComponent } from './hotel-facilities.component';

describe('HotelFacilitiesComponent', () => {
  let component: HotelFacilitiesComponent;
  let fixture: ComponentFixture<HotelFacilitiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HotelFacilitiesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HotelFacilitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
