import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MealPlanService } from '../../services/meal-plan.service';
import { CustomMealService } from '../../services/custom-meal.service';
import { MealPlan, MealType, MealItem } from '../../shared/models/meal-plan.model';

@Component({
  selector: 'app-meal-entry',
  standalone: true,
  imports: [
    RouterLink,
    DatePipe,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './meal-entry.component.html',
  styleUrl: './meal-entry.component.scss'
})
export class MealEntryComponent implements OnInit {
  mealForm!: FormGroup;
  date!: Date;
  mealType!: MealType;
  mealTypes = Object.values(MealType);
  customMeals: any[] = [];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private mealPlanService: MealPlanService,
    private customMealService: CustomMealService
  ) {
    this.initForm();
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.date = new Date(params['date']);
      this.mealType = params['type'] as MealType;
      this.loadCustomMeals();
      this.loadExistingMeal();
    });
  }

  private initForm() {
    this.mealForm = this.fb.group({
      mealType: ['', Validators.required],
      notes: [''],
      mealItems: this.fb.array([])
    });
  }

  private loadCustomMeals() {
    this.customMealService.getCustomMeals().subscribe({
      next: (meals) => this.customMeals = meals,
      error: (error) => console.error('Error loading custom meals:', error)
    });
  }

  private loadExistingMeal() {
    // TODO: Implement loading existing meal if editing
  }

  addMealItem() {
    // TODO: Implement adding meal item to form array
  }

  removeMealItem(index: number) {
    // TODO: Implement removing meal item from form array
  }

  onSubmit() {
    if (this.mealForm.valid) {
      const mealPlan: MealPlan = {
        id: 0, // New meal plan
        date: this.date,
        mealType: this.mealForm.value.mealType,
        notes: this.mealForm.value.notes,
        mealItems: this.mealForm.value.mealItems
      };

      this.mealPlanService.saveMealPlan(mealPlan).subscribe({
        next: () => this.router.navigate(['/week']),
        error: (error) => console.error('Error saving meal plan:', error)
      });
    }
  }
}
