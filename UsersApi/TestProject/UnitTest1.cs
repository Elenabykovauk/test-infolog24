using System.Collections.Generic;
using NUnit.Framework;
using UsersApi.Domain;
using Moq;

namespace TestProject;

public class Tests
{
    [Test]
    public void TestUserService()
    {
        // Arrange
        Mock<IUserDataService> userDataService = new Mock<IUserDataService>();
        Mock<IPositionDataService> positionDataService = new Mock<IPositionDataService>();

        userDataService.Setup(x => x.GetAll())
            .Returns(GetUserList());
        positionDataService.Setup(x => x.GetAll())
            .Returns(GetPositionList());

        var expected = GetExpectedUserList();
        var service = new UserService(userDataService.Object, positionDataService.Object);

        // Act
        var actual = service.GetUserList();

        // Assert
        Assert.IsFalse(expected.Equals(actual));
    }

    private static List<UserDto> GetExpectedUserList()
    {
        return new List<UserDto>
        {
            new UserDto
            {
                Id = "1",
                Name = "N",
                Login = "M",
                DefaultSalary = 22,
                Position = "rr"
            },
            new UserDto
            {
                Id = "3",
                Name = "R",
                Login = "R",
                DefaultSalary = 22,
                Position = "rr"
            },
            new UserDto
            {
                Id = "3",
                Name = "S",
                Login = "S",
                DefaultSalary = 22,
                Position = "rr"
            }
        };
    }
    
    private List<Position> GetPositionList()
    {
        return new List<Position>
        {
            new Position
            {
                Id = "1",
                DefaultSalary = 22,
                Name = "rr"
            }
        };
    }
    private List<User> GetUserList()
    {
        return new List<User>() {
            new User
            {
                Id = "1",
                Name = "N",
                Login = "M",
                PositionId = "1"
            },
            new User{
                Id = "3",
                Name = "R",
                Login = "R",
                PositionId = "1"
            },
            new User{
                Id = "3",
                Name = "S",
                Login = "S",
                PositionId = "1"
            }
        };
    }
}