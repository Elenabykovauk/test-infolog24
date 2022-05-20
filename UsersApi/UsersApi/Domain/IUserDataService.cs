using System.Collections.Generic;

namespace UsersApi.Domain;

public interface IUserDataService
{
    List<User> GetAll();

    long GetCount();
}