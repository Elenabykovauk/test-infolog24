using System.Collections.Generic;
using System.Linq;

namespace UsersApi.Domain
{
    public class UserService :IUserService
    {
        private readonly IUserDataService _userDataService;
        private readonly IPositionDataService _positionDataService;

        public UserService(IUserDataService userDataService,
            IPositionDataService positionDataService) {
            _userDataService = userDataService;
            _positionDataService = positionDataService;
        }
        
        public List<UserDto> GetUserList()
        {
            var users = _userDataService.GetAll();
            var positions = _positionDataService.GetAll();

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

            return joinList;
        }
        
        public long GetUserCount()
        {
            var users = new UserDataService().GetCount();

            return users;
        }
    }
}