import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListofdepartmentComponent } from './listofdepartment.component';

describe('ListofdepartmentComponent', () => {
  let component: ListofdepartmentComponent;
  let fixture: ComponentFixture<ListofdepartmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListofdepartmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListofdepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
