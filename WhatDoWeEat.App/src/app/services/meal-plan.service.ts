import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MealPlan } from '../shared/models/meal-plan.model';
import { environment } from '../../../../src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MealPlanService {
  private apiUrl = `${environment.apiUrl}/api/meal-plans`;

  constructor(private http: HttpClient) {}

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
