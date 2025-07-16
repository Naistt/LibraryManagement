import { TestBed } from '@angular/core/testing';
import { BookStoreService } from './book-store.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('BookStoreService', () => {
  let service: BookStoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(BookStoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
