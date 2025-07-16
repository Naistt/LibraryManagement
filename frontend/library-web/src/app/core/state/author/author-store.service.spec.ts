import { TestBed } from '@angular/core/testing';
import { AuthorStoreService } from './author-store.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AuthorStoreService', () => {
  let service: AuthorStoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(AuthorStoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
