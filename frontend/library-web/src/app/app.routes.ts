import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { GenreListComponent } from './features/genres/genre-list/genre-list.component';
import { AuthorsListComponent } from './features/authors/authors-list.component';
import { BooksListComponent } from './features/books/books-list.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'authors', component: AuthorsListComponent },
  { path: 'genres', component: GenreListComponent },
  { path: 'books', component: BooksListComponent },
  { path: '**', redirectTo: 'home' },
  { path: '', redirectTo: 'home', pathMatch: 'full' }
];
