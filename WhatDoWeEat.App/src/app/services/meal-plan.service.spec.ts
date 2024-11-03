import { createHttpFactory, HttpMethod, SpectatorHttp } from '@ngneat/spectator';
import { MealPlanService } from './meal-plan.service';
import { MealPlan, MealType } from '../shared/models/meal-plan.model';
import { AppConfig } from '../config/app-config';

describe('MealPlanService', () => {
  let spectator: SpectatorHttp<MealPlanService>;
  const createHttp = createHttpFactory({
    service: MealPlanService,
    providers: [
      {
        provide: AppConfig,
        useValue: { apiUrl: 'http://localhost:5000' }
      }
    ]
  });

  beforeEach(() => spectator = createHttp());

  it('should be created', () => {
    expect(spectator.service).toBeTruthy();
  });

  it('should get weekly meal plan', () => {
    const weekNumber = 1;
    spectator.service.getWeeklyPlan(weekNumber).subscribe();

    spectator.expectOne(
      `http://localhost:5000/api/meal-plans/${weekNumber}`,
      HttpMethod.GET
    );
  });

  it('should save meal plan', () => {
    const mealPlan: MealPlan = {
      id: 1,
      date: new Date(),
      mealType: MealType.Breakfast,
      mealItems: []
    };

    spectator.service.saveMealPlan(mealPlan).subscribe();

    const req = spectator.expectOne(
      'http://localhost:5000/api/meal-plans',
      HttpMethod.POST
    );
    expect(req.request.body).toEqual(mealPlan);
  });

  it('should delete meal plan', () => {
    const id = 1;
    spectator.service.deleteMealPlan(id).subscribe();

    spectator.expectOne(
      `http://localhost:5000/api/meal-plans/${id}`,
      HttpMethod.DELETE
    );
  });
});
