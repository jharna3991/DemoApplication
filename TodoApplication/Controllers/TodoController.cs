﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Data;
using TodoApplication.Features.TodoFeatures.Queries;

namespace TodoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class TodoController : Controller
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _mediator.Send(new GetAllTodoListQuery()));
        }

        [Route("Details")]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var obj = await _mediator.Send(new GetTodoByIdQuery() { Id = id });

            if (obj is null)
            {
                return BadRequest("No Record Found");
            }
            return Ok(obj);
        }
    }
}