# Card Actions

Card Actions is a RESTful API designed to determine and return the allowed actions for a specified card. It is built using .NET and follows modern API development practices.

## Features

- Retrieve allowed actions for a given card.
- Modular architecture with clear separation of concerns.
- Unit tests to ensure reliability and maintainability.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or higher)
- A code editor like [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository:
```sh
   https://github.com/MrKrasucki/card-actions.git
   cd card-actions
```
2. Navigate to the API project directory:
```sh
cd src/CardActions.API
```

3. Restore dependencies:
```sh
dotnet restore
```

### Running the API

1. Start the API:
```sh
dotnet run
```

2. The API will be available at http://localhost:5194 by default.
3. Swagger page will be available at http://localhost:5194/swagger/index.html address.

### Running Tests

1. Navigate to the test project directory:
```sh
cd tests/CardActions.Tests
```
2. Run the tests:
```sh
dotnet test
```

# My comments

I've addressed the requirements of the task:
- I've built a simple .NET minimal API with .NET8 and C#. I tried to make it as simplistic as possible so it's easy to read and navigate.
- Input data is protected with FluentValidation and CardService before trying to find the allowed actions.
- In case of any unexpected issue while getting allowed actions, ActionsService will silently fail returning empty array and log the error message.
- Project structure is clearly separated by the purpose and domain parts are sructured vertically - it helps with navigation and separation of the features

I've chosen InMemory database so the project can be run without installation of any third-party tool like Docker. 
Database is seeded with permissions matrix data on the startup.

## Future improvements:
- Containerizing the application
- Implementing real database connection
- Building authentication/authorization
- Depending on the system specifics permissions matrix could be cached and kept in-memory