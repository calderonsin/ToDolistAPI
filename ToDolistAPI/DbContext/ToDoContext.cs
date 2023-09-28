using Microsoft.EntityFrameworkCore;
using ToDolistAPI.Models;
namespace ToDolistAPI.DbContext
{
    public class ToDoContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }

        public DbSet<ToDoItem> TodoItems { get; set; }
    }
}
