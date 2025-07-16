import { HttpErrorResponse, HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { MessageService } from 'primeng/api';
import { tap } from 'rxjs';

export const httpInterceptor: HttpInterceptorFn = (req, next) => {
  const messageService = inject(MessageService);

  const clonedRequest = req.clone({
    setHeaders: {
    }
  });

  return next(clonedRequest).pipe(
    tap({
      next: (event) => {
        if (event instanceof HttpResponse) {
          console.log('Resposta recebida:', event);
        }
      },
      error: (error: HttpErrorResponse) => {
        console.error('Erro na requisição:', error);
      }
    })
  );
};
