export interface MealPlan {
  id: number;
  date: Date;
  mealType: MealType;
  notes?: string;
  mealItems: MealItem[];
}

export interface MealItem {
  type: 'product' | 'recipe' | 'custom';
  id: number;
  quantity?: number;
}

export enum MealType {
  Breakfast = 'breakfast',
  Lunch = 'lunch',
  Snack = 'snack',
  Dinner = 'dinner'
} 