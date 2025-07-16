import { Component } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CommonModule } from '@angular/common';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, ButtonModule, CardModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  animations: [
    trigger('fadeInUp', [
      state('void', style({
        opacity: 0,
        transform: 'translateY(30px)'
      })),
      transition(':enter', [
        animate('0.6s ease-out')
      ])
    ])
  ]
})
export class HomeComponent {
  cards = [
    {
      title: 'Livros',
      subtitle: 'Gerenciar Biblioteca',
      description: 'Visualize, adicione, edite e remova livros da biblioteca. Mantenha seu acervo organizado e atualizado.',
      icon: 'pi pi-book',
      route: '/books',
      color: 'success'
    },
    {
      title: 'Autores',
      subtitle: 'Gerenciar Autores',
      description: 'Cadastre e gerencie informações sobre autores. Mantenha um registro completo dos escritores.',
      icon: 'pi pi-user',
      route: '/authors',
      color: 'success'
    },
    {
      title: 'Gêneros',
      subtitle: 'Gerenciar Categorias',
      description: 'Organize seus livros por gêneros literários. Crie e gerencie categorias para melhor classificação.',
      icon: 'pi pi-tags',
      route: '/genres',
      color: 'success'
    }
  ];

  constructor(private router: Router) {}

  navigateTo(route: string): void {
    this.router.navigate([route]);
  }
}
