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
    public class GetAllTodoListQuery: IRequest<IEnumerable<TodoModel>>
    {
        public class GetAllTodoListQueryHandler : IRequestHandler<GetAllTodoListQuery, IEnumerable<TodoModel>>
        {
            private readonly ITodoListService _todoListService;
            public GetAllTodoListQueryHandler(ITodoListService todoListService)
            {
                _todoListService = todoListService;
            }

            public async Task<IEnumerable<TodoModel>> Handle(GetAllTodoListQuery request, CancellationToken cancellationToken)
            {
                return await _todoListService.GetTodoList();
            }
        }
    }
}
