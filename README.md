# ToDoListAPI

The ToDoListAPI is a RESTful API built using ASP.NET Core 6 and Entity Framework Core, designed to manage a simple todo list. It allows users to create, read, update, and delete todo items.

## Prerequisites

Before running the ToDoListAPI, make sure you have the following prerequisites installed on your development machine:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)


## Getting Started

Follow these steps to set up and run the ToDoListAPI:

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/ToDoListAPI.git
   cd ToDoListAPI
   dotnet build
   dotnet run


The API will be accessible at https://localhost:5001 by default.


Database Configuration
The ToDoListAPI uses Entity Framework Core to persist todo items in a database. 
To configure a different database, update the connection string in the appsettings.json file.


API Endpoints
GET /api/todoitems: Get a list of all todo items.
GET /api/todoitems/{id}: Get a specific todo item by its ID.
POST /api/todoitems: Create a new todo item.
PUT /api/todoitems/{id}: Update a todo item by its ID.
DELETE /api/todoitems/{id}: Delete a todo item by its ID.



Usage Examples
You can use tools like Postman or curl to interact with the API endpoints. Here are some example requests:


Create a Todo Item (POST)
POST https://localhost:5001/api/todoitems
Content-Type: application/json

{
    "title": "Buy groceries",
    "description": "Buy milk, eggs, and bread."
}

Get All Todo Items (GET)
GET https://localhost:5001/api/todoitems

Update a Todo Item (PUT)
PUT https://localhost:5001/api/todoitems/1
Content-Type: application/json

{
    "id": 1,
    "title": "Buy groceries",
    "description": "Buy milk, eggs, bread, and cheese."
}

Delete a Todo Item (DELETE)
DELETE https://localhost:5001/api/todoitems/1



- Was it easy to complete the task using AI? R/ Sort of, sometimes chatgpt give weird anwers and when I asked about deepers concepts about EF it gave incorrect answers.
- How long did task take you to complete? (Please be honest, we need it to gather anonymized statistics) R/ around 10 hours.
- Was the code ready to run after generation? What did you have to change to make it usable? R/ I have to configure the conections strings and some nugetpackages and start some services in progam.cs
- Which challenges did you face during completion of the task? R/ unity test.
- Which specific prompts you learned as a good practice to complete the task? R/ give a lot of context and give a output format.
