# SPEC-001: Weekly Meal Planner App (Local Web App)

## Background

This project aims to create a local web-based meal planning application that leverages data from the user's local Grocy client. Grocy provides product and recipe management, allowing the user to track ingredients and inventory. This meal planner app will pull data from Grocy to help plan meals for each day of the week, with support for multiple meals a day (e.g., breakfast, snack, and dinner). 

Future enhancements may include AI-powered meal suggestions based on available ingredients, user preferences, and historical data.

## Requirements

* The app should allow the user to plan meals for an entire week, supporting multiple meals per day:
  * Workdays: Breakfast, Snack, Dinner
  * Weekends: Breakfast, Lunch, Snack, Dinner

* The app must:
  * Pull product and recipe data from the local Grocy client.
  * Enable the entry of custom meal data that can appear as future suggestions.
  * Display the week’s meal plan in a list view to accommodate smaller screens and provide a clean, non-cluttered interface.
  * Support a print-friendly view that allows users to print a weekly meal plan.

* Optional future feature:
  * AI-powered meal suggestions based on the Grocy inventory and custom entries.

## Method

### Architecture Overview

The app will be a single-page local web application, structured with Angular for the frontend and a C# backend API for managing data interactions with the local Grocy client. We’ll use the following structure:

* **Frontend**: 
  * Angular (latest version) for a responsive UI that allows smooth navigation of the week’s meals in a list view format.
  * HTML/CSS for layout and styling, optimized for smaller screens.

* **Backend**:
  * C# (.NET latest version) for handling data retrieval from the Grocy API and managing local data storage.
  * SQLite database to store custom entries and meal plans locally on the user's device.

### Grocy Data Integration

The app will connect to the Grocy API to pull product and recipe data as needed. Users will be able to trigger data retrieval on demand, and results will be stored temporarily in the local database (SQLite) for fast access and potential offline functionality.

1. **Data Retrieval**:
   * Angular will make requests to the C# backend API to fetch Grocy data (recipes, products) as needed.
2. **Caching**: The fetched data will be cached in the local SQLite database to avoid repeated API calls and enable fast data access.

### Custom Data Storage

Custom meal entries are simplified, with only `id` and `name` attributes required. These entries will be saved in an SQLite database, where they can be easily retrieved and displayed as suggestions during the planning process.

**Database Schema**:
We’ll define the following tables in SQLite:

1. `CustomMeals`:
   * `id`: Unique identifier
   * `name`: Name of the custom meal

2. `MealPlans`:
   * `id`: Unique identifier
   * `date`: The date of the meal
   * `meal_type`: Type of meal (e.g., breakfast, lunch)
   * `notes`: Optional notes for each meal entry
   * `meal_items`: JSON field to store an array of products/recipes for a given meal, allowing multiple entries per meal

**MealPlan Structure**:
The `meal_items` field in `MealPlans` allows flexibility to store multiple products/recipes for one meal. Each entry in this array can hold:
   * `type`: Specifies whether it’s a product, recipe, or custom entry.
   * `id`: References the product, recipe, or custom meal ID.
   * `quantity`: Optional field for specifying quantities of items (useful if planning specific serving sizes).

### Print-Friendly View

To support printing, the app will provide a "Print Week" feature that generates a clean, formatted view of the week's meal plan. This view will include:
* **Week Number**: Displayed at the top of the print layout.
* **Daily Meal Plan**: Each day’s planned meals listed in a simple format (e.g., “Breakfast: [Meal Names]”).

The print view will omit unnecessary UI elements to ensure readability and focus on the essentials.

## Implementation

### Step 1: Setup Environment

1. **Frontend (Angular)**:
   * Install Angular CLI and create a new Angular project.
     ```bash
     npm install -g @angular/cli
     ng new MealPlannerApp
     ```
   * Set up the Angular app structure, organizing components for:
     * **Week View Component**: Displays the planned meals for the week.
     * **Meal Entry Component**: Allows users to add or edit meals for specific days and meal types.
     * **Print View Component**: Generates a formatted print layout.

2. **Backend (C# .NET)**:
   * Set up a new .NET Web API project.
     ```bash
     dotnet new webapi -o MealPlannerApi
     ```
   * Install necessary packages for SQLite integration and JSON handling.
     ```bash
     dotnet add package Microsoft.EntityFrameworkCore.Sqlite
     dotnet add package Microsoft.EntityFrameworkCore.Design
     dotnet add package Newtonsoft.Json
     ```

3. **Database (SQLite)**:
   * Initialize an SQLite database and create tables `CustomMeals` and `MealPlans` based on the schema defined in the Method section.
   * Write Entity Framework Core (EF Core) models for `CustomMeals` and `MealPlans`.
   * Apply initial database migrations.
     ```bash
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

### Step 2: Backend API Development

1. **Grocy Data Endpoints**:
   * Create API endpoints in the C# backend to fetch product and recipe data from Grocy. This can include endpoints like `/api/grocy/products` and `/api/grocy/recipes`.
   * Implement Grocy API calls using `HttpClient` and parse the data for frontend consumption.

2. **Local Data Management**:
   * Set up endpoints for handling custom meal entries and meal plans.
   * API Endpoints:
     * **POST /api/custom-meals**: Add a new custom meal.
     * **GET /api/custom-meals**: Retrieve all custom meals for suggestions.
     * **POST /api/meal-plans**: Create or update a meal plan for a specific day and meal type.
     * **GET /api/meal-plans/{weekNumber}**: Retrieve meal plans for a specific week.
     * **DELETE /api/meal-plans/{id}**: Delete a meal plan entry.

3. **Database Integration**:
   * Implement data access methods using Entity Framework Core for `CustomMeals` and `MealPlans`.
   * Ensure data validation and error handling are in place for robust API functionality.

### Step 3: Frontend Development (Angular Components)

1. **Week View Component**:
   * Create a component to display each day of the week, with sections for breakfast, snack, dinner, etc.
   * Use Angular’s `HttpClient` to fetch meal plans and display each meal’s name based on the API data.
   * Allow the user to click on a meal slot to open the Meal Entry component for adding or editing entries.

2. **Meal Entry Component**:
   * Set up a form to add products, recipes, or custom meal entries to a specific meal slot.
   * Allow search and selection from Grocy products, Grocy recipes, or custom meals.
   * On save, send the updated meal plan to the backend via the `POST /api/meal-plans` endpoint.

3. **Print View Component**:
   * Build a simple layout that shows the week number, each day, and planned meals.
   * Use Angular’s print method or a custom stylesheet to trigger a print-friendly format.
   * Allow user to print the view by triggering `window.print()` when they click “Print Week”.

### Step 4: Testing and Debugging

1. **Unit Testing**:
   * Write unit tests for both Angular components and C# backend API endpoints.
   * Use Angular’s `Jasmine` and `Karma` frameworks for frontend tests.
   * Use `.NET`’s xUnit or NUnit for backend testing.

2. **Integration Testing**:
   * Test data fetching from Grocy and ensure the app handles empty or incomplete data correctly.
   * Validate that custom entries and meal plans are correctly stored and retrieved.

3. **End-to-End Testing**:
   * Simulate a complete user flow, from planning a week’s meals to printing the weekly plan.
   * Test print view functionality to ensure formatting is clear on different screen sizes.

### Step 5: Deployment and Local Setup

1. **Local Deployment**:
   * Package the app for local use, configuring C# to run as a local service.
   * Ensure the SQLite database is accessible in the app directory for easy data access.

2. **Documentation**:
   * Create a README file with setup instructions and usage notes.
   * Include instructions for configuring the Grocy API connection and customizing local storage options.

## Milestones

1. **Milestone 1: Environment Setup and Initial Configuration**
   * Set up Angular project structure and C# backend API.
   * Initialize SQLite database and create tables for `CustomMeals` and `MealPlans`.
   * Confirm Angular and C# backend can connect and exchange basic test data.

2. **Milestone 2: Backend API Development**
   * Develop Grocy data integration endpoints and ensure they fetch recipes/products as needed.
   * Complete endpoints for managing `CustomMeals` and `MealPlans`.
   * Test API functionality with sample data and validate CRUD operations.

3. **Milestone 3: Frontend Component Development**
   * Develop **Week View Component** to display daily meal slots in a list view.
   * Implement **Meal Entry Component** for adding/editing meal plans, integrating Grocy data, and custom entries.
   * Test frontend interactions, ensuring seamless data flow from backend to frontend.

4. **Milestone 4: Print View and Data Caching**
   * Implement the **Print View Component** with a clean weekly layout.
   * Enable caching of Grocy data in SQLite for offline access.
   * Validate printing functionality, ensuring the weekly layout is readable.

5. **Milestone 5: Testing and Final Adjustments**
   * Conduct unit, integration, and end-to-end testing for frontend and backend.
   * Debug issues, optimize performance, and ensure smooth user experience.
   * Finalize documentation and prepare app for local deployment.

6. **Milestone 6: Deployment and User Guide**
   * Package and deploy the app for local use.
   * Provide a user guide, including setup steps, usage instructions, and troubleshooting tips.

## Gathering Results

* After deployment, measure success based on:
  * User feedback on ease of use for weekly planning.
  * Performance of data fetching and print functionality.
  * Smooth integration with Grocy and reliability of cached data in SQLite.
  * Ability to easily add and retrieve custom entries as suggestions.
