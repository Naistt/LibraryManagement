import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Author } from '../../../shared/models/author.model';
import { AuthorService } from '../../services/author/author.service';

@Injectable({ providedIn: 'root' })
export class AuthorStoreService {
  private authorsSubject = new BehaviorSubject<Author[]>([]);
  authors$ = this.authorsSubject.asObservable();

  constructor(private authorService: AuthorService) {}

  loadAuthors(): void {
    this.authorService.getAllDetailed().subscribe(response => {
      if (response.success && response.data) {
        this.authorsSubject.next(response.data);
      }
    });
  }

  addAuthorLocally(newAuthor: Author): void {
    const current = this.authorsSubject.getValue();
    this.authorsSubject.next([...current, newAuthor]);
  }

  updateAuthorLocally(updated: Author): void {
    const current = this.authorsSubject.getValue();
    const updatedList = current.map(a => (a.id === updated.id ? updated : a));
    this.authorsSubject.next(updatedList);
  }

  deleteAuthorLocally(id: number): void {
    const current = this.authorsSubject.getValue();
    const updatedList = current.filter(a => a.id !== id);
    this.authorsSubject.next(updatedList);
  }

  clear(): void {
    this.authorsSubject.next([]);
  }
}
