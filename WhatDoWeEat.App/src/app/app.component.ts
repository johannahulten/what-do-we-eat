import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatModules } from './shared/mat-modules';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ...MatModules],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'meal-planner-app';
}
