import { ComponentFixture, TestBed } from '@angular/core/testing';
import { GenreListComponent } from './genre-list.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ConfirmationService, MessageService } from 'primeng/api';

describe('GenreListComponent', () => {
  let component: GenreListComponent;
  let fixture: ComponentFixture<GenreListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenreListComponent, HttpClientTestingModule],
      providers: [MessageService, ConfirmationService],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(GenreListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
