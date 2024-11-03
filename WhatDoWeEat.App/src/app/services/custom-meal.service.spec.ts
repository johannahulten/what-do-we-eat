import { createHttpFactory, HttpMethod, SpectatorHttp } from '@ngneat/spectator';
import { CustomMealService } from './custom-meal.service';
import { AppConfig } from '../config/app-config';

describe('CustomMealService', () => {
  let spectator: SpectatorHttp<CustomMealService>;
  const createHttp = createHttpFactory({
    service: CustomMealService,
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

  it('should get all custom meals', () => {
    spectator.service.getCustomMeals().subscribe();

    spectator.expectOne(
      'http://localhost:5000/api/custom-meals',
      HttpMethod.GET
    );
  });

  it('should create custom meal', () => {
    const meal = { name: 'Test Meal' };
    spectator.service.createCustomMeal(meal).subscribe();

    const req = spectator.expectOne(
      'http://localhost:5000/api/custom-meals',
      HttpMethod.POST
    );
    expect(req.request.body).toEqual(meal);
  });

  it('should get custom meal by id', () => {
    const id = 1;
    spectator.service.getCustomMeal(id).subscribe();

    spectator.expectOne(
      `http://localhost:5000/api/custom-meals/${id}`,
      HttpMethod.GET
    );
  });
});
