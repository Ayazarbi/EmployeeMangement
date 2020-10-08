import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditdepartmentComponent } from './editdepartment.component';

describe('EditdepartmentComponent', () => {
  let component: EditdepartmentComponent;
  let fixture: ComponentFixture<EditdepartmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditdepartmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditdepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
