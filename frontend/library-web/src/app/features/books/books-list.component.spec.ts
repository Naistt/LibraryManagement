import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BooksListComponent } from './books-list.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ConfirmationService, MessageService } from 'primeng/api';

describe('BooksListComponent', () => {
  let component: BooksListComponent;
  let fixture: ComponentFixture<BooksListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BooksListComponent, HttpClientTestingModule],
      providers: [MessageService, ConfirmationService],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(BooksListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
