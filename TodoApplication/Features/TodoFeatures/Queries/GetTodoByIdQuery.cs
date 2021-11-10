using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoApplication.Models;
using TodoApplication.Services;

namespace TodoApplication.Features.TodoFeatures.Queries
{
    public class GetTodoByIdQuery : IRequest<TodoModel>
    {
        public int Id { get; set; }
        public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoModel>
        {

            private readonly ITodoListService _todoListService;
            public GetTodoByIdQueryHandler(ITodoListService todoListService)
            {
                _todoListService = todoListService;
            }
            public async Task<TodoModel> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
            {
                return await _todoListService.GetTodo(query.Id);
            }
        }
    }
}
