# ShoppingApp

## Overview

ShoppingApp is a multi-layered Web API designed to facilitate online shopping operations. It provides endpoints for managing products, orders, customers, and other related entities, enabling seamless integration with front-end applications.

## Tech Stack

- **Framework**: ASP.NET Core
- **Data Access**: Entity Framework Core
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity with JWT
- **Architecture**: Multi-layered (Web API, Business Logic, Data Access)

## Features

- **User Authentication & Authorization**: Secure login and role-based access control using JWT.
- **Product Management**: CRUD operations for products.
- **Order Processing**: Handling customer orders and order details.
- **Customer Management**: Managing customer information and profiles.
- **Middleware Integration**: Custom middleware for logging and exception handling.
- **Model Validation**: Ensuring data integrity through comprehensive validation.

## Project Structure

The solution is organized into the following projects:

- **ShoppingApp.WebApi**: Contains the API controllers and startup configuration.
- **ShoppingApp.Business**: Implements the business logic and service interfaces.
- **ShoppingApp.Data**: Manages data access, including Entity Framework Core DbContext and migrations.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/AenuHub/ShoppingApp.git
   cd ShoppingApp
   ```

2. **Configure the database**:
   - Update the connection string in `appsettings.json` to point to your SQL Server instance.

3. **Apply migrations**:
   ```bash
   dotnet ef database update --project ShoppingApp.Data
   ```

4. **Run the application**:
   ```bash
   dotnet run --project ShoppingApp.WebApi
   ```

## API Endpoints

### Authentication

- **POST** `/api/Auth/register`: Register a new user.
- **POST** `/api/Auth/login`: Login/Authenticate and receive a JWT.

### Products
- **POST** `/api/Products/add-product`: Create a new product.

### Orders

- **GET** `/api/Orders/all-orders`: Retrieve all orders.
- **GET** `/api/Orders/{id}`: Retrieve an order by ID.
- **POST** `/api/Orders/create-order`: Create a new order.
- **PUT** `/api/Orders/update-order/{id}`: Update an order using ID.
- **PATCH** `/api/Orders/patch-order/{id}`: Patch an order using ID.
- **DELETE** `/api/Orders/delete-order/{id}`: Delete an order.

### Settings
- **PATCH** `/api/Settings`: Toggle maintenance mode on/off.

## Middleware

The application includes custom middleware for:

- **Maintenance Mode**: Putting system into maintenance mode by enabling/disabling entire system.
- **Exception Handling**: Centralized error handling and response formatting.

## Action Filter
Action filter is used for handling time values for accessing only permitted endpoints between the set time values.

## Model Validation

Data models are validated using data annotations and custom validation attributes to ensure data integrity and consistency.
