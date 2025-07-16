import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Author } from '../../../shared/models/author.model';
import { BaseResponse } from '../../../shared/models/base-response.model';
import { GenreViewModel } from '../../../shared/models/genre-view.model';

@Injectable({ providedIn: 'root' })
export class AuthorService {
  private apiUrl = environment.apiUrl + '/Author';

  constructor(private http: HttpClient) {}

  getAll(): Observable<BaseResponse<Author[]>> {
    return this.http.get<BaseResponse<Author[]>>(this.apiUrl);
  }

  getAllDetailed(): Observable<BaseResponse<GenreViewModel[]>> {
    return this.http.get<BaseResponse<GenreViewModel[]>>(`${this.apiUrl}/detailed`);
  }  

  getById(id: number): Observable<BaseResponse<Author>> {
    return this.http.get<BaseResponse<Author>>(`${this.apiUrl}/${id}`);
  }

  create(data: any): Observable<BaseResponse<Author>> {
    return this.http.post<BaseResponse<Author>>(this.apiUrl, data);
  }

  update(id: number, data: any): Observable<BaseResponse<Author>> {
    return this.http.put<BaseResponse<Author>>(`${this.apiUrl}/${id}`, data);
  }

  delete(id: number): Observable<BaseResponse<Author>> {
    return this.http.delete<BaseResponse<Author>>(`${this.apiUrl}/${id}`);
  }
}
