using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Data;
using TodoApplication.Models;

namespace TodoApplication.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly ApplicationDbContext _db;
        public TodoListService(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateTodo(TodoModel todo)
        {
            _db.TodoList.Add(todo);
            return Save();
        }

        public async Task<TodoModel> GetTodo(int Id)
        {
            return await _db.TodoList.FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<IEnumerable<TodoModel>> GetTodoList()
        {
            return await _db.TodoList.ToListAsync();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool TodoExists(string name)
        {
            bool value = _db.TodoList.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool TodoExists(int id)
        {
            bool value = _db.TodoList.Any(a => a.Id == id);
            return value;
        }
        
        public bool UpdateTodo(TodoModel todo)
        {
            _db.TodoList.Update(todo);
            return Save();
        }

        
    }
}
