import { createComponentFactory, Spectator } from '@ngneat/spectator';
import { WeekViewComponent } from './week-view.component';
import { MealPlanService } from '../../services/meal-plan.service';
import { MealType } from '../../shared/models/meal-plan.model';
import { of } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

describe('WeekViewComponent', () => {
  let spectator: Spectator<WeekViewComponent>;
  const mockMealPlanService = {
    getWeeklyPlan: jest.fn()
  };

  const createComponent = createComponentFactory({
    component: WeekViewComponent,
    imports: [MatCardModule, MatIconModule],
    providers: [
      { provide: MealPlanService, useValue: mockMealPlanService }
    ]
  });

  beforeEach(() => {
    spectator = createComponent();
    mockMealPlanService.getWeeklyPlan.mockReset();
  });

  it('should create', () => {
    expect(spectator.component).toBeTruthy();
  });

  it('should load meal plans on init', () => {
    mockMealPlanService.getWeeklyPlan.mockReturnValue(of([]));
    spectator.component.ngOnInit();
    expect(mockMealPlanService.getWeeklyPlan).toHaveBeenCalled();
  });

  it('should correctly identify weekends', () => {
    expect(spectator.component.isWeekend('Saturday')).toBeTruthy();
    expect(spectator.component.isWeekend('Sunday')).toBeTruthy();
    expect(spectator.component.isWeekend('Monday')).toBeFalsy();
  });

  it('should navigate weeks correctly', () => {
    mockMealPlanService.getWeeklyPlan.mockReturnValue(of([]));
    const initialWeek = spectator.component.currentWeek;

    spectator.component.navigateWeek('next');
    expect(spectator.component.currentWeek.getTime()).toBeGreaterThan(initialWeek.getTime());

    spectator.component.navigateWeek('prev');
    expect(spectator.component.currentWeek.getTime()).toBe(initialWeek.getTime());
  });

  it('should return correct meal types for weekdays and weekends', () => {
    const weekdayMeals = spectator.component.mealTypes.weekday;
    const weekendMeals = spectator.component.mealTypes.weekend;

    expect(weekdayMeals).toContain(MealType.Breakfast);
    expect(weekdayMeals).toContain(MealType.Snack);
    expect(weekdayMeals).toContain(MealType.Dinner);
    expect(weekdayMeals).not.toContain(MealType.Lunch);

    expect(weekendMeals).toContain(MealType.Breakfast);
    expect(weekendMeals).toContain(MealType.Lunch);
    expect(weekendMeals).toContain(MealType.Snack);
    expect(weekendMeals).toContain(MealType.Dinner);
  });
});
