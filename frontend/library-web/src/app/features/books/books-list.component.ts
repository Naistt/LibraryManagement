import { Component, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { Book } from '../../shared/models/book.model';
import { ConfirmationService, MessageService } from 'primeng/api';
import { BookService } from '../../core/services/book/book.service';
import { AuthorService } from '../../core/services/author/author.service';
import { GenreService } from '../../core/services/genre/genre.service';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { FloatLabelModule } from 'primeng/floatlabel';
import { TextareaModule } from 'primeng/textarea';
import { BookStoreService } from '../../core/state/book/book-store.service';
import { Author } from '../../shared/models/author.model';
import { Genre } from '../../shared/models/genre.model';
import { CommonModule } from '@angular/common';
import { SelectModule } from 'primeng/select';

@Component({
  selector: 'app-books-list',
  standalone: true,
  imports: [
    CommonModule,
    TableModule,
    ToastModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    ConfirmDialogModule,
    ReactiveFormsModule,
    FormsModule,
    FloatLabelModule,
    TextareaModule,
    SelectModule
  ],
  providers: [ConfirmationService],
  templateUrl: './books-list.component.html',
  styleUrl: './books-list.component.scss'
})
export class BooksListComponent implements OnInit {

  books: Book[] = [];
  authors: Author[] = [];
  genres: Genre[] = [];
  loading: boolean = true;

  bookDialog = false;
  editing = false;
  bookForm: FormGroup;
  selectedBookId: number | null = null;

  constructor(
    private bookStore: BookStoreService,
    private bookService: BookService,
    private authorService: AuthorService,
    private genreService: GenreService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private fb: FormBuilder,
  ) {
    this.bookForm = this.fb.group({
      title: ['', Validators.required],
      summary: [''],
      publicationYear: [null],
      authorId: [null, Validators.required],
      genreId: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    this.bookStore.loadBooks();
    this.fetchBooks();
    this.authorService.getAll().subscribe(res => this.authors = res.data ?? []);
    this.genreService.getAll().subscribe(res => this.genres = res.data ?? []);
  }

  fetchBooks(): void {
    this.loading = true;
    this.bookStore.books$.subscribe(books => {
      this.books = books;
      this.books.forEach(book => {
        
      });
      this.loading = false;
    });
  }

  openNew(): void {
    this.bookForm.reset();
    this.selectedBookId = null;
    this.editing = false;
    this.bookDialog = true;
  }

  editBook(book: Book): void {
    this.bookForm.patchValue(book);
    this.selectedBookId = book.id;
    this.editing = true;
    this.bookDialog = true;
  }

  saveBook(): void {
    if (this.bookForm.invalid) return;

    const formValue = this.bookForm.value;

    if (this.editing && this.selectedBookId !== null) {
      this.bookService.update(this.selectedBookId, formValue).subscribe({
        next: () => {
          this.bookStore.updateBookLocally({ id: this.selectedBookId!, ...formValue });
          this.messageService.add({ severity: 'success', summary: 'Atualizado', detail: 'Livro atualizado com sucesso!' });
          this.bookDialog = false;
          this.bookStore.loadBooks();
          this.fetchBooks();
        },
        error: () => this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao atualizar livro.' }),
      });
    } else {
      this.bookService.create(formValue).subscribe({
        next: (res) => {
          this.bookStore.addBookLocally(res.data);
          this.messageService.add({ severity: 'success', summary: 'Criado', detail: 'Livro criado com sucesso!' });
          this.bookDialog = false;
          this.bookStore.loadBooks();
          this.fetchBooks();
        },
        error: () => this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao criar livro.' }),
      });
    }
  }

  confirmDelete(book: Book): void {
    this.confirmationService.confirm({
      message: `Deseja excluir o livro "${book.title}"?`,
      header: 'Confirmar Exclusão',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.bookService.delete(book.id).subscribe({
          next: () => {
            this.messageService.add({ severity: 'success', summary: 'Excluído', detail: 'Livro removido com sucesso!' });
            this.bookStore.loadBooks();
            this.fetchBooks();
          },
          error: () => this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao excluir livro.' }),
        });
      },
    });
  }

  getAuthorName(authorId: number): string {
    const author = this.authors.find(a => a.id === authorId);
    return author ? author.name : 'N/A';
  }

  getGenreName(genreId: number): string {
    const genre = this.genres.find(g => g.id === genreId);
    return genre ? genre.name : 'N/A';
  }
}
