using System.Collections.Generic;

namespace UsersApi.Domain;

public interface IPositionDataService
{
    List<Position> GetAll();
    Position GetById(string id);
}