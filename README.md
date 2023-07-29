# Content Management System (CMS) - .NET Core 5 Project

## Introduction

Welcome to the Content Management System (CMS) project! This is a robust and scalable CMS built using .NET Core 5 and various technologies like Redis, MySQL, Entity Framework, JWT authentication, and follows monolith architecture. The project includes four main components: API, Business, Data, and Infrastructure projects.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Features

- User-friendly API endpoints to manage content.
- Role-based access control using JWT authentication.
- Efficient data storage and retrieval with Redis and MySQL.
- Separated monolith architecture for better maintainability.
- Extensible and customizable business logic.

## Technologies Used

- .NET Core 5: The foundation of the project, providing cross-platform compatibility and performance.
- Redis: In-memory data store for caching frequently accessed data, enhancing performance.
- MySQL: Database management system for persistent data storage.
- Entity Framework: Object-Relational Mapping (ORM) tool for database interactions.
- JWT Authentication: Secure and stateless authentication mechanism using JSON Web Tokens.
- Dependency Injection: Managing and resolving dependencies efficiently.

## Getting Started

To run this CMS project locally or in your own environment, follow the steps below.

### Prerequisites

- [.NET Core 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) installed on your machine.
- [Docker](https://www.docker.com/) (optional) if you prefer to use containers for running the database services.

### Installation

1. Clone this GitHub repository to your local machine using the following command:

```
git clone https://github.com/mavzerbay/MAV.Cms.git
```

2. Navigate to the project directory:

```
cd MAV.Cms
```

3. Restore the dependencies by running:

```
dotnet restore
```

4. Build the solution:

```
dotnet build
```

### Configuration

The CMS project requires some configuration before it can be run successfully. The configuration files are located in the respective projects (`API`, `Business`, `Data`, and `Infrastructure`). You need to provide the necessary settings for the following:

- Database connection string for MySQL (in `appsettings.json` or `appsettings.{environment}.json`).
- Redis configuration (in `appsettings.json` or `appsettings.{environment}.json`).
- JWT authentication settings (in `appsettings.json` or `appsettings.{environment}.json`).

Make sure to adjust these settings according to your local development or deployment environment.

## Project Structure

The CMS project follows a separated monolith architecture to ensure maintainability and scalability. The project structure is as follows:

- `API`: Contains the Web API controllers and related configurations.
- `Business`: Implements the business logic and services.
- `Data`: Defines data models, repositories, and database interactions using Entity Framework.
- `Infrastructure`: Contains cross-cutting concerns, such as caching and logging.

```
/MAV.Cms
    ├── API
    ├── Business
    ├── Data
    └── Infrastructure
```

Feel free to explore each project to get a better understanding of their responsibilities.

## Contributing

Contributions to the CMS project are welcome! If you find any issues or want to add new features, please open an issue or submit a pull request. We appreciate your feedback and support!

## License

This CMS project is licensed under the [MIT License](LICENSE). You are free to use, modify, and distribute the code as per the terms of the license.

---

Thank you for choosing the Content Management System (CMS) project. We hope this system meets your expectations and helps you efficiently manage your content. If you have any questions or need assistance, please don't hesitate to contact us or open an issue on GitHub. Happy coding!
