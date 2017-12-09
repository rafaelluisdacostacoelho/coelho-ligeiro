using Microsoft.AspNetCore.Mvc;
using NosEmpreendedores.Application.Interfaces;
using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;
using System;
using System.Collections.Generic;

namespace NosEmpreendedores.WebApi.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public void Create([FromBody]UserRequest model)
        {
            userService.Create(model);
        }

        [HttpGet]
        public IEnumerable<UserResponse> Read()
        {
            return userService.Read();
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]UserRequest model)
        {
            userService.Update(id, model);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            userService.Delete(id);
        }

        [HttpGet("{id}")]
        public UserResponse GetById(Guid id)
        {
            return userService.GetById(id);
        }
    }
}