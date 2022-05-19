using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Domain;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class UsersController : ControllerBase
    {
        [Route("list")]
        public IActionResult GetList()
        {
            var response = new UserResponse
            {
                Users = new List<UserDto>()
            };

            var userService = new UserService();
            var users = userService.GetAll();
            response.Total = users.Count;
            for (int i = 1; i < users.Count; i++)
            {
                var user = users[i];
                var dto = new UserDto
                {
                    Id = user.Id,
                    Login = user.Login,
                    Name = user.Name
                };
                var position = new PositionService().GetById(user.PositionId);
                dto.Position = position.Name;
                dto.DefaultSalary = position.DefaultSalary;
                response.Users.Add(dto);
            }
            return Ok(response);
        }
    }
}