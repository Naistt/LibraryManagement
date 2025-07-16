import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Genre } from '../../../shared/models/genre.model';
import { BaseResponse } from '../../../shared/models/base-response.model';
import { GenreViewModel } from '../../../shared/models/genre-view.model';

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private apiUrl = environment.apiUrl + '/Genre';

  constructor(private http: HttpClient) {}

  getAll(): Observable<BaseResponse<Genre[]>> {
    return this.http.get<BaseResponse<Genre[]>>(this.apiUrl);
  }

  getAllDetailed(): Observable<BaseResponse<GenreViewModel[]>> {
    return this.http.get<BaseResponse<GenreViewModel[]>>(`${this.apiUrl}/detailed`);
  }  

  getById(id: number): Observable<BaseResponse<Genre>> {
    return this.http.get<BaseResponse<Genre>>(`${this.apiUrl}/${id}`);
  }

  create(data: any): Observable<BaseResponse<Genre>> {
    return this.http.post<BaseResponse<Genre>>(this.apiUrl, data);
  }

  update(id: number, data: any): Observable<BaseResponse<Genre>> {
    return this.http.put<BaseResponse<Genre>>(`${this.apiUrl}/${id}`, data);
  }

  delete(id: number): Observable<BaseResponse<Genre>> {
    return this.http.delete<BaseResponse<Genre>>(`${this.apiUrl}/${id}`);
  }
}
