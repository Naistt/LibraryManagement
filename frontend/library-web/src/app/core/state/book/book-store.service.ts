import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Book } from '../../../shared/models/book.model';
import { BookService } from '../../services/book/book.service';

@Injectable({ providedIn: 'root' })
export class BookStoreService {
  private booksSubject = new BehaviorSubject<Book[]>([]);
  books$ = this.booksSubject.asObservable();

  constructor(private bookService: BookService) {}

  loadBooks(): void {
    this.bookService.getAll().subscribe(response => {
      if (response.success && response.data) {
        this.booksSubject.next(response.data);
      }
    });
  }

  addBookLocally(newBook: Book): void {
    const current = this.booksSubject.getValue();
    this.booksSubject.next([...current, newBook]);
  }

  updateBookLocally(updated: Book): void {
    const current = this.booksSubject.getValue();
    const updatedList = current.map(b => (b.id === updated.id ? updated : b));
    this.booksSubject.next(updatedList);
  }

  deleteBookLocally(id: number): void {
    const current = this.booksSubject.getValue();
    const updatedList = current.filter(b => b.id !== id);
    this.booksSubject.next(updatedList);
  }

  clear(): void {
    this.booksSubject.next([]);
  }
}
