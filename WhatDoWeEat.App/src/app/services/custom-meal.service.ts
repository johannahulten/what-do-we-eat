import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppConfig } from '../config/app-config';

export interface CustomMeal {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class CustomMealService {
  private appConfig = inject(AppConfig);
  private http = inject(HttpClient);

  private apiUrl = `${this.appConfig.apiUrl}/api/custom-meals`;

  getCustomMeals(): Observable<CustomMeal[]> {
    return this.http.get<CustomMeal[]>(this.apiUrl);
  }

  createCustomMeal(meal: Omit<CustomMeal, 'id'>): Observable<CustomMeal> {
    return this.http.post<CustomMeal>(this.apiUrl, meal);
  }

  getCustomMeal(id: number): Observable<CustomMeal> {
    return this.http.get<CustomMeal>(`${this.apiUrl}/${id}`);
  }
}
