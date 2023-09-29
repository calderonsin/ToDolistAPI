using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using ToDolistAPI.Controllers;
using ToDolistAPI.DbContext;
using ToDolistAPI.Models;

namespace ToDoList
{
    public class ToDoControllerTests: IDisposable
    {
        [SetUp]
        public void Setup()
        {
        }
        private ToDoController _ToDoController;
        private ToDoContext _ToDoContext;
        private DbContextOptions<ToDoContext> _options = new DbContextOptionsBuilder<ToDoContext>().UseInMemoryDatabase(databaseName:"TestDatabase").Options;
        

        public ToDoControllerTests() {
        _ToDoContext = new ToDoContext(_options);
        _ToDoController = new ToDoController(_ToDoContext);

        }

        [Test]
        public async Task ToDoController_GetToDoItems_ReturnIEnumerable()
        {
            //Arrange
            var items = new List<ToDoItem>()
            {
                new ToDoItem { Title = "Task 1", Description = "Description 1" },
                new ToDoItem { Title = "Task 2", Description = "Description 2" },
            };
            using (var _ToDoContext = new ToDoContext(_options))
            {
            
            await _ToDoContext.AddRangeAsync(items);
            await _ToDoContext.SaveChangesAsync();
            
            };

            //Act
            var result = await _ToDoController.GetToDoItems();


            //Assert
            Assert.IsInstanceOf<IEnumerable<ToDoItem>>(result.Value);            
           
          
        }

        [Test]
        public async Task ToDoController_GetToDoItem_ReturnTodoItem()
        {
            //Arrange
            var id = 1;
            var items = new List<ToDoItem>()
            {
                new ToDoItem { Title = "Task 1", Description = "Description 1" },
                new ToDoItem { Title = "Task 2", Description = "Description 2" },
            };
            using (var _ToDoContext = new ToDoContext(_options))
            {

                await _ToDoContext.AddRangeAsync(items);
                await _ToDoContext.SaveChangesAsync();
            };
            

            //Act
            var result = await _ToDoController.GetToDoItem(id);

            //Assert
            
            Assert.IsInstanceOf<ToDoItem>(result.Value);


        }

        [Test]
        public async Task ToDoController_PostToDoItem_ReturnTodoItem()
        {
            //Arrange
            var id = 1;
            var item = new ToDoItem
            {
                Title = "Task 1", Description = "Description 1"
                
            };
            using (var _ToDoContext = new ToDoContext(_options))
            {

                await _ToDoContext.AddRangeAsync(item);
                await _ToDoContext.SaveChangesAsync();
            };

            var item1 = new ToDoItem
            {
                Title = "Task 1",
                Description = "Description 1"

            };

            //Act
            var result = await _ToDoController.PostToDoItem(item1);

            //Assert
            // Assert.That(result.Value,Is.InstanceOf<ToDoItem>());
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            // Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);


        }

        public void Dispose()
        {
            using (var context = new ToDoContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}