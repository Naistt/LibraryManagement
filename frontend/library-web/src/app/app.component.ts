import { Component } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { ToastModule } from 'primeng/toast';
import { MenubarModule } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { MenuItem } from 'primeng/api';
import { NavbarComponent } from './shared/components/navbar/navbar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,
    RouterModule, 
    ToastModule,
    MenubarModule,
    ButtonModule,
    DialogModule,
    NavbarComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'library-web';
  isDarkMode = false;
  showInfoDialog = false;

  menuItems: MenuItem[] = [
    {
      label: 'Home',
      icon: 'pi pi-home',
      routerLink: '/home'
    },
    {
      label: 'Autores',
      icon: 'pi pi-user',
      routerLink: '/authors'
    },
    {
      label: 'Gêneros',
      icon: 'pi pi-tags',
      routerLink: '/genres'
    },
    {
      label: 'Livros',
      icon: 'pi pi-book',
      routerLink: '/books'
    }
  ];

  toggleTheme() {
    this.isDarkMode = !this.isDarkMode;
    document.body.classList.toggle('dark-mode', this.isDarkMode);
  }
}
