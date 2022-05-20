using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Domain;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) {
            _userService = userService;
        }
        
        [Route("list")]
        public IActionResult GetList()
        {
            var response = new UserResponse();
            
            var userList = _userService.GetUserList();

            response.Total = userList.Count;
            response.Users = userList;
            
            return Ok(response);
        }
        
        [Route("oldlist")]
        public IActionResult GetOldList()
        {
            var response = new UserResponse
            {
                Users = new List<UserDto>()
            };

            var users = new UserDataService().GetAll();
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
                var position = new PositionDataService().GetById(user.PositionId);
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
                        Id = "9287adc0e0b287b9c64b" + i,
                        Name = "MTI" + i,
                        Login = "MTI" + i,
                        PositionId = "6287adc0e0b287b9c64b17b4"
                    }
                );
            }

            return Ok(result);
        }
    }
}