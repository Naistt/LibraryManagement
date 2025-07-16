import { TestBed } from '@angular/core/testing';
import { GenreStoreService } from './genre-store.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('GenreStoreService', () => {
  let service: GenreStoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(GenreStoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
