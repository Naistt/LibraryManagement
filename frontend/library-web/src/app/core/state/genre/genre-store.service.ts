import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Genre } from '../../../shared/models/genre.model';
import { GenreService } from '../../services/genre/genre.service';
import { GenreViewModel } from '../../../shared/models/genre-view.model';

@Injectable({
  providedIn: 'root'
})
export class GenreStoreService {
  private genresSubject = new BehaviorSubject<GenreViewModel[]>([]);
  genres$ = this.genresSubject.asObservable();

  constructor(private genreService: GenreService) {}

  loadGenres(): void {
    this.genreService.getAllDetailed().subscribe({
      next: (res) => {
        this.genresSubject.next(res.data ?? []);
      },
      error: () => {
        this.genresSubject.next([]);
      },
    });
  }

  get currentGenres(): GenreViewModel[] {
    return this.genresSubject.getValue();
  }

  addGenreLocally(newGenre: GenreViewModel): void {
    this.genresSubject.next([...this.currentGenres, newGenre]);
  }

  updateGenreLocally(updated: GenreViewModel): void {
    const updatedList = this.currentGenres.map(g => (g.id === updated.id ? updated : g));
    this.genresSubject.next(updatedList);
  }

  deleteGenreLocally(id: number): void {
    const updatedList = this.currentGenres.filter(g => g.id !== id);
    this.genresSubject.next(updatedList);
  }

  clear(): void {
    this.genresSubject.next([]);
  }
}
