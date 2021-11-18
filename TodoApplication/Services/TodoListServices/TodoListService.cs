using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Data;
using TodoApplication.Models;

namespace TodoApplication.Services.TodoListServices
{
    public class TodoListService : ITodoListService
    {
        private readonly ApplicationDbContext _db;
        public TodoListService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<TodoModel> CreateTodo(TodoModel todo)
        {
            _db.TodoList.Add(todo);
            await _db.SaveChangesAsync();
            return todo;
        }

        public async Task<TodoModel> GetTodo(int Id)
        {
            return await _db.TodoList.FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<IEnumerable<TodoModel>> GetTodoList()
        {
            //return await _db.TodoList.ToListAsync();
            return await _db.TodoList.ToListAsync();
        }

        public async Task<int> UpdateTodo(TodoModel todo)
        {
            _db.TodoList.Update(todo);
            return await _db.SaveChangesAsync();
        }

    }
}
