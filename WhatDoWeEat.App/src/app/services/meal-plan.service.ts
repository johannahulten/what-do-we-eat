import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MealPlan } from '../shared/models/meal-plan.model';
import { AppConfig } from '../config/app-config';

@Injectable({
  providedIn: 'root'
})
export class MealPlanService {
  private appConfig = inject(AppConfig);
  private http = inject(HttpClient);

  private apiUrl = `${this.appConfig.apiUrl}/api/meal-plans`;

  getWeeklyPlan(weekNumber: number): Observable<MealPlan[]> {
    return this.http.get<MealPlan[]>(`${this.apiUrl}/${weekNumber}`);
  }

  saveMealPlan(mealPlan: MealPlan): Observable<MealPlan> {
    return this.http.post<MealPlan>(this.apiUrl, mealPlan);
  }

  deleteMealPlan(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
