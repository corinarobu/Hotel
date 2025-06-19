import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSearchbarComponent } from './admin-searchbar.component';

describe('AdminSearchbarComponent', () => {
  let component: AdminSearchbarComponent;
  let fixture: ComponentFixture<AdminSearchbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminSearchbarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminSearchbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
