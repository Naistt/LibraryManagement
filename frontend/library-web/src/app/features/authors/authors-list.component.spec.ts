import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthorsListComponent } from './authors-list.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ConfirmationService, MessageService } from 'primeng/api';

describe('AuthorsListComponent', () => {
  let component: AuthorsListComponent;
  let fixture: ComponentFixture<AuthorsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthorsListComponent, HttpClientTestingModule],
      providers: [MessageService, ConfirmationService],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(AuthorsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
