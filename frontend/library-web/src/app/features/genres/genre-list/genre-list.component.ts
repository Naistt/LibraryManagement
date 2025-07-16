import { Component, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { Genre } from '../../../shared/models/genre.model';
import { ConfirmationService, MessageService } from 'primeng/api';
import { GenreService } from '../../../core/services/genre/genre.service';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { FloatLabelModule } from 'primeng/floatlabel';
import { TextareaModule } from 'primeng/textarea';
import { GenreStoreService } from '../../../core/state/genre/genre-store.service';
import { Observable } from 'rxjs';
import { GenreViewModel } from '../../../shared/models/genre-view.model';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-genre-list',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TableModule,
    ToastModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    ConfirmDialogModule,
    ReactiveFormsModule,
    FormsModule,
    FloatLabelModule,
    TextareaModule
  ],
  providers: [ConfirmationService],
  templateUrl: './genre-list.component.html',
  styleUrl: './genre-list.component.scss'
})
export class GenreListComponent implements OnInit {

  genres$!: Observable<GenreViewModel[]>;
  loading$!: Observable<boolean>;

  
  loading: boolean = true;
  genres: GenreViewModel[] = [];

  genreDialog = false;
  editing = false;
  genreForm: FormGroup;
  selectedGenreId: number | null = null;


  constructor(
    private genreStore: GenreStoreService,
    private genreService: GenreService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private fb: FormBuilder,
  ) {
    this.genreForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
    });
  }


  ngOnInit(): void {
    this.genreStore.loadGenres();
    this.fetchGenres();
  }

  fetchGenres(): void {
    this.loading = true;
    this.genreStore.genres$.subscribe(genres => {
      this.genres = genres;
      this.loading = false;
    });
  }

  openNew(): void {
    this.genreForm.reset();
    this.selectedGenreId = null;
    this.editing = false;
    this.genreDialog = true;
  }

  editGenre(genre: Genre): void {
    this.genreForm.patchValue(genre);
    this.selectedGenreId = genre.id;
    this.editing = true;
    this.genreDialog = true;
  }

  saveGenre(): void {
    if (this.genreForm.invalid) return;

    const formValue = this.genreForm.value;

    if (this.editing && this.selectedGenreId !== null) {
      this.genreService.update(this.selectedGenreId, formValue).subscribe({
        next: () => {
          this.genreStore.updateGenreLocally(formValue);
          this.messageService.add({ severity: 'success', summary: 'Atualizado', detail: 'Gênero atualizado com sucesso!' });
          this.genreDialog = false;
          this.genreStore.loadGenres();
          this.fetchGenres();
        },
        error: () => this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao atualizar gênero.' }),
      });
    } else {
      this.genreService.create(formValue).subscribe({
        next: () => {
          this.genreStore.addGenreLocally(formValue);
          this.messageService.add({ severity: 'success', summary: 'Criado', detail: 'Gênero criado com sucesso!' });
          this.genreDialog = false;
          this.genreStore.loadGenres();
          this.fetchGenres();
        },
        error: () => this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao criar gênero.' }),
      });
    }
  }

  confirmDelete(genre: Genre): void {
    this.confirmationService.confirm({
      message: `Deseja excluir o gênero "${genre.name}"?`,
      header: 'Confirmar Exclusão',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.genreService.delete(genre.id).subscribe({
          next: () => {
            this.messageService.add({ severity: 'success', summary: 'Excluído', detail: 'Gênero removido com sucesso!' });
            this.genreStore.loadGenres();
            this.fetchGenres();
          },
          error: () => this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao excluir gênero.' }),
        });
      },
    });
  }
}
