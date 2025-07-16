import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  imports: [MenubarModule, ButtonModule, DialogModule],
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  items = [
    { label: 'Início', icon: 'pi pi-home', routerLink: '/home' },
    { label: 'Gêneros', icon: 'pi pi-tags', routerLink: '/genres' },
    { label: 'Livros', icon: 'pi pi-book', routerLink: '/books' },
    { label: 'Autores', icon: 'pi pi-users', routerLink: '/authors' }
  ];

  isDark = false;
  infoDialog = false;

  constructor(private router: Router) {}

  toggleTheme() {
    const element = document.querySelector('html');
    if (element) {
      element.classList.toggle('my-app-dark');
      this.isDark = element.classList.contains('my-app-dark');
    }
  }

  showInfo() {
    this.infoDialog = true;
  }
}
