import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuotationReceivedComponent } from './quotation-received.component';

describe('QuotationReceivedComponent', () => {
  let component: QuotationReceivedComponent;
  let fixture: ComponentFixture<QuotationReceivedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuotationReceivedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuotationReceivedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
