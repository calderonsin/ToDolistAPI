using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToDolistAPI.Controllers;
using ToDolistAPI.DbContext;
using ToDolistAPI.Models;

namespace ToDoList
{
    public class ToDoControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }
        private ToDoController _ToDoController;
        private ToDoContext _ToDoContext;
        
        public ToDoControllerTests() {
            var options = new DbContextOptionsBuilder<ToDoContext>().UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;

            _ToDoContext = new ToDoContext(options);

            _ToDoController = new ToDoController(_ToDoContext);

        }

        [Test]
        public async Task ToDoController_GetToDoItems_ReturnSuccess()
        {
            //Arrange
            var items = new List<ToDoItem>()
            {
                 new ToDoItem { Id = 1, Title = "Task 1", Description = "Description 1" },
                new ToDoItem { Id = 2, Title = "Task 2", Description = "Description 2" },
            };
            A.CallTo(() => _ToDoContext.TodoItems.ToListAsync()).Returns(items);

            //Act
            var result = await _ToDoController.GetToDoItems();

            //Assert
            Assert.IsInstanceOf<ToDoItem>(result);
           
          
        }
    }
}