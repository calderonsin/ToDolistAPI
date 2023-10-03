using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ToDolistAPI.Controllers;
using ToDolistAPI.DbContext;
using ToDolistAPI.Models;

namespace ToDoList
{
    using System;
    using NUnit.Framework;
    public class ToDoControllerTests : IDisposable
    {
        private DbContextOptions<ToDoContext> options;
        private ToDoContext ToDoContext;
        private ToDoController _ToDoController;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ToDoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString(), new InMemoryDatabaseRoot())
                .Options;
            ToDoContext = new ToDoContext(options);
            _ToDoController = new ToDoController(ToDoContext);
        }



        [TearDown] public void Cleanup() {
            Dispose();

        }

            [Test]
        public async Task ToDoController_GetToDoItems_ReturnIEnumerable()
        {
            //Arrange
            var items = new List<ToDoItem>()
            {
                new ToDoItem { Title = "Task 1", Description = "Description 1" },
                new ToDoItem { Title = "Task 2", Description = "Description 2" },
                new ToDoItem { Title = "Task 3", Description = "Description 3" },
            };
            using (var _ToDoContext = new ToDoContext(options))
            {
            
            await _ToDoContext.AddRangeAsync(items);
            await _ToDoContext.SaveChangesAsync();
            
            };

            //Act
            var result = await _ToDoController.GetToDoItems();


            //Assert
            Assert.IsInstanceOf<IEnumerable<ToDoItem>>(result.Value);
            Assert.That(result.Value.Count,Is.EqualTo(3));
           
          
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
            using (var _ToDoContext = new ToDoContext(options))
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
            var item = new ToDoItem
            {
                Title = "Task 1", Description = "Description 1"
                
            };
            var item1 = new ToDoItem
            {
                Title = "Task 1",
                Description = "Description 1"

            };
            using (var _ToDoContext = new ToDoContext(options))
            {

                await _ToDoContext.AddAsync(item);
                await _ToDoContext.SaveChangesAsync();
            };

            

            //Act
            var result = await _ToDoController.PostToDoItem(item1);            

            //Assert         
           
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);            



        }

        public async Task ToDoController_PutToDoItem_Return()
        {
            //Arrange
            var id = 1;
            var item = new ToDoItem
            {
                Id = id,
                Title = "Task 1",
                Description = "Description 1"

            };
            using (var _ToDoContext = new ToDoContext(options))
            {

                await _ToDoContext.AddAsync(item);
                await _ToDoContext.SaveChangesAsync();
            };

            var ItemToUpdated = new ToDoItem
            {
                Title = "Title update",
                Description = "Description Update"

            };

            //Act
            var result = await _ToDoController.PutToDoItem(id,ItemToUpdated);

            //Assert
            using (var context = new ToDoContext(options))
            {
                var updatedItem = await context.TodoItems.FindAsync(id);
                Assert.IsNotNull(updatedItem);
                Assert.AreEqual("Title update", updatedItem.Title);
                Assert.AreEqual("Description Update", updatedItem.Description);
            }        



        }

        public void Dispose()
        {
            using (var context = new ToDoContext(options))
            {
                if (context.Database.IsInMemory()){
                    context.Database.EnsureDeleted();
                }
                
            }
        }
    }
}