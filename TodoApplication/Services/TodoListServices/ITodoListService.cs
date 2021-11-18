using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Models;

namespace TodoApplication.Services.TodoListServices
{
    public interface ITodoListService
    {
        Task<IEnumerable<TodoModel>> GetTodoList();
        Task <TodoModel> GetTodo(int Id);
        Task<TodoModel> CreateTodo(TodoModel todo);
        Task<int> UpdateTodo(TodoModel todo);
    }
}
