using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoApplication.Models;
using TodoApplication.Services;
using TodoApplication.Services.TodoListServices;

namespace TodoApplication.Features.TodoFeatures.Commands
{
    public class CreateTodoCommand : IRequest<TodoModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool State { get; set; }
        public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoModel>
        {
            private readonly ITodoListService _todoListService;

            public CreateTodoCommandHandler(ITodoListService todoListService)
            {
                _todoListService = todoListService;
            }

            public async Task<TodoModel> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
            {
                var todo = new TodoModel()
                {
                    Name = command.Name,
                    Description = command.Description,
                    State = command.State
                };
                return await _todoListService.CreateTodo(todo);
            }
        }
    }
}
