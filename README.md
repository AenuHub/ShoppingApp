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

The API will be accessible at `https://localhost:5001`.

## API Endpoints

### Authentication

- **POST** `/api/auth/register`: Register a new user.
- **POST** `/api/auth/login`: Authenticate and receive a JWT.

### Products

- **GET** `/api/products`: Retrieve all products.
- **GET** `/api/products/{id}`: Retrieve a product by ID.
- **POST** `/api/products`: Create a new product.
- **PUT** `/api/products/{id}`: Update an existing product.
- **DELETE** `/api/products/{id}`: Delete a product.

### Orders

- **GET** `/api/orders`: Retrieve all orders.
- **GET** `/api/orders/{id}`: Retrieve an order by ID.
- **POST** `/api/orders`: Create a new order.
- **PUT** `/api/orders/{id}`: Update an order.
- **DELETE** `/api/orders/{id}`: Delete an order.

## Middleware

The application includes custom middleware for:

- **Logging**: Capturing request and response information.
- **Exception Handling**: Centralized error handling and response formatting.

## Model Validation

Data models are validated using data annotations and custom validation attributes to ensure data integrity and consistency.
