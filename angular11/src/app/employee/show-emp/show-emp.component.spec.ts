import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowEmpComponent } from './show-emp.component';

describe('ShowComponent', () => {
  let component: ShowEmpComponent;
  let fixture: ComponentFixture<ShowEmpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowEmpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
