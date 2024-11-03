import { Routes } from '@angular/router';
import { WeekViewComponent } from './components/week-view/week-view.component';
import { PrintViewComponent } from './components/print-view/print-view.component';
import { MealEntryComponent } from './components/meal-entry/meal-entry.component';

export const routes: Routes = [
  { path: '', redirectTo: '/week', pathMatch: 'full' },
  { path: 'week', component: WeekViewComponent },
  { path: 'entry', component: MealEntryComponent },
  { path: 'print', component: PrintViewComponent }
];
