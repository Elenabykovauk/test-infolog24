using System.Collections.Generic;

namespace UsersApi.Domain
{
    public interface IUserService
    {
        List<UserDto> GetUserList();
        long GetUserCount();
    }
}