import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Book } from '../../../shared/models/book.model';
import { BaseResponse } from '../../../shared/models/base-response.model';

@Injectable({ providedIn: 'root' })
export class BookService {
  private apiUrl = environment.apiUrl + '/Book';

  constructor(private http: HttpClient) {}

  getAll(): Observable<BaseResponse<Book[]>> {
    return this.http.get<BaseResponse<Book[]>>(this.apiUrl);
  }

  getById(id: number): Observable<BaseResponse<Book>> {
    return this.http.get<BaseResponse<Book>>(`${this.apiUrl}/${id}`);
  }

  create(data: any): Observable<BaseResponse<Book>> {
    return this.http.post<BaseResponse<Book>>(this.apiUrl, data);
  }

  update(id: number, data: any): Observable<BaseResponse<Book>> {
    return this.http.put<BaseResponse<Book>>(`${this.apiUrl}/${id}`, data);
  }

  delete(id: number): Observable<BaseResponse<Book>> {
    return this.http.delete<BaseResponse<Book>>(`${this.apiUrl}/${id}`);
  }
}
