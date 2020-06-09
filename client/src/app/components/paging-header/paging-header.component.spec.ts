import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PagingHeaderComponent } from './paging-header.component';

describe('PagingHeaderComponent', () => {
  let component: PagingHeaderComponent;
  let fixture: ComponentFixture<PagingHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PagingHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PagingHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
