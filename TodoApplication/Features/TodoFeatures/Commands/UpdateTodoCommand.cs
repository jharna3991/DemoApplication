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
    public class UpdateTodoCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }

        public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, int>
        {
            private readonly ITodoListService _todoListService;

            public UpdateTodoCommandHandler(ITodoListService todoListService)
            {
                _todoListService = todoListService;
            }

            public async Task<int> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
            {
                var todo = await _todoListService.GetTodo(command.Id);

                todo.Name = command.Name;
                todo.Description = command.Description;
                todo.State = command.State;

                return await _todoListService.UpdateTodo(todo);
            }

        }
    }
}
