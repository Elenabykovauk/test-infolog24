using System.Collections.Generic;
using System.Linq;
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
            var response = new UserResponse();

            var userService = new UserService();
            var users = userService.GetAll();
            var positions = new PositionService().GetAll();

            var joinList = users.GroupJoin(positions,
                    u => u.PositionId,
                    p => p.Id,
                    (u, p) => new { u, p })
                .SelectMany(
                    o => o.p.DefaultIfEmpty(),
                    (u, p) => new UserDto
                    {
                        Id = u.u.Id,
                        Login = u.u.Login,
                        Name = u.u.Name,
                        Position = p?.Name,
                        DefaultSalary = p?.DefaultSalary ?? 0
                    }).ToList();
            
            response.Total = users.Count;
            response.Users = joinList;
            
            return Ok(response);
        }
        
        [Route("oldlist")]
        public IActionResult GetOldList()
        {
            var response = new UserResponse
            {
                Users = new List<UserDto>()
            };

            var userService = new UserService();
            var users = userService.GetAll();
            response.Total = users.Count;
            for (int i = 0; i < users.Count; i++)
            {
                var user = users[i];
                var dto = new UserDto
                {
                    Id = user.Id,
                    Login = user.Login,
                    Name = user.Name
                };
                var position = new PositionService().GetById(user.PositionId);
                if (position != null)
                {
                    dto.Position = position.Name;
                    dto.DefaultSalary = position.DefaultSalary;
                }

                response.Users.Add(dto);
            }
            return Ok(response);
        }
            [Route("testFile")]
            public IActionResult GetTestFile()
            {
                var result = new List<User>();
                for (var i = 0; i < 10000; i++)
                {
                    result.Add(
                        new User
                        {
                            Id = "9287adc0e0b287b9c64b"+i,
                            Name = "MTI"+i,
                            Login = "MTI"+i,
                            PositionId = "6287adc0e0b287b9c64b17b4"
                        }
                        );
                }
              
                return Ok(result);
            }
    }
}