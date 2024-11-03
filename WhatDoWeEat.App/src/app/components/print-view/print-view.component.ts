import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MealPlanService } from '../../services/meal-plan.service';
import { MealPlan, MealType } from '../../shared/models/meal-plan.model';
import { startOfWeek, getISOWeek } from 'date-fns';

@Component({
  selector: 'app-print-view',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule],
  templateUrl: './print-view.component.html',
  styleUrl: './print-view.component.scss'
})
export class PrintViewComponent implements OnInit {
  weekDays = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];
  mealTypes = {
    weekday: [MealType.Breakfast, MealType.Snack, MealType.Dinner],
    weekend: [MealType.Breakfast, MealType.Lunch, MealType.Snack, MealType.Dinner]
  };

  currentWeek: Date = startOfWeek(new Date(), { weekStartsOn: 1 });
  mealPlans: MealPlan[] = [];

  constructor(private mealPlanService: MealPlanService) {}

  ngOnInit() {
    this.loadWeekMealPlans();
  }

  loadWeekMealPlans() {
    const weekNumber = getISOWeek(this.currentWeek);
    this.mealPlanService.getWeeklyPlan(weekNumber).subscribe({
      next: (plans) => this.mealPlans = plans,
      error: (error) => console.error('Error loading meal plans:', error)
    });
  }

  getMealsForDay(date: Date, mealType: MealType): MealPlan | undefined {
    return this.mealPlans.find(plan =>
      plan.date.toString() === date.toString() &&
      plan.mealType === mealType
    );
  }

  isWeekend(day: string): boolean {
    return day === 'Saturday' || day === 'Sunday';
  }

  printWeek() {
    window.print();
  }

  getWeekNumber(): number {
    return getISOWeek(this.currentWeek);
  }
}
