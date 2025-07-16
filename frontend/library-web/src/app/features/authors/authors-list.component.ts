import { Component, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { Author } from '../../shared/models/author.model';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AuthorService } from '../../core/services/author/author.service';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { FloatLabelModule } from 'primeng/floatlabel';
import { TextareaModule } from 'primeng/textarea';
import { AuthorStoreService } from '../../core/state/author/author-store.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-authors-list',
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
    TextareaModule
  ],
  providers: [ConfirmationService],
  templateUrl: './authors-list.component.html',
  styleUrl: './authors-list.component.scss'
})
export class AuthorsListComponent implements OnInit {

  authors: Author[] = [];
  loading: boolean = true;

  authorDialog = false;
  editing = false;
  authorForm: FormGroup;
  selectedAuthorId: number | null = null;

  constructor(
    private authorStore: AuthorStoreService,
    private authorService: AuthorService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private fb: FormBuilder,
  ) {
    this.authorForm = this.fb.group({
      name: ['', Validators.required],
      bio: [''],
    });
  }

  ngOnInit(): void {
    this.authorStore.loadAuthors();
    this.fetchAuthors();
  }

  fetchAuthors(): void {
    this.loading = true;
    this.authorStore.authors$.subscribe(authors => {
      this.authors = authors;
      this.loading = false;
    });
  }

  openNew(): void {
    this.authorForm.reset();
    this.selectedAuthorId = null;
    this.editing = false;
    this.authorDialog = true;
  }

  editAuthor(author: Author): void {
    this.authorForm.patchValue(author);
    this.selectedAuthorId = author.id;
    this.editing = true;
    this.authorDialog = true;
  }

  saveAuthor(): void {
    if (this.authorForm.invalid) return;

    const formValue = this.authorForm.value;

    if (this.editing && this.selectedAuthorId !== null) {
      this.authorService.update(this.selectedAuthorId, formValue).subscribe({
        next: () => {
          this.authorStore.updateAuthorLocally({ id: this.selectedAuthorId!, ...formValue });
          this.messageService.add({ severity: 'success', summary: 'Atualizado', detail: 'Autor atualizado com sucesso!' });
          this.authorDialog = false;
          this.authorStore.loadAuthors();
          this.fetchAuthors();
        },
        error: () => this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao atualizar autor.' }),
      });
    } else {
      this.authorService.create(formValue).subscribe({
        next: (res) => {
          this.authorStore.addAuthorLocally(res.data);
          this.messageService.add({ severity: 'success', summary: 'Criado', detail: 'Autor criado com sucesso!' });
          this.authorDialog = false;
          this.authorStore.loadAuthors();
          this.fetchAuthors();
        },
        error: () => this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao criar autor.' }),
      });
    }
  }

  confirmDelete(author: Author): void {
    this.confirmationService.confirm({
      message: `Deseja excluir o autor "${author.name}"?`,
      header: 'Confirmar Exclusão',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.authorService.delete(author.id).subscribe({
          next: () => {
            this.messageService.add({ severity: 'success', summary: 'Excluído', detail: 'Autor removido com sucesso!' });
            this.authorStore.loadAuthors();
            this.fetchAuthors();
          },
          error: () => this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao excluir autor.' }),
        });
      },
    });
  }
}
