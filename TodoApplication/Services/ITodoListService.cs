using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Models;

namespace TodoApplication.Services
{
    public interface ITodoListService
    {
        Task<IEnumerable<TodoModel>> GetTodoList();
        Task <TodoModel> GetTodo(int Id);
        bool TodoExists(string name);
        bool TodoExists(int id);
        bool CreateTodo(TodoModel todo);
        bool UpdateTodo(TodoModel todo);
        bool Save();
    }
}
