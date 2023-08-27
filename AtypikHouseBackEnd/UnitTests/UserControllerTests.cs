using AtypikHouseBackEnd;
using AtypikHouseBackEnd.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Payloads;
using Repositories;

namespace UnitTests;

[TestClass]
public class UserControllerTests
{
    [TestMethod]
    public void CreateUser()
    {
        WebApplication application = TestsUtils.CreateContext();
        UserController controller = application.Services.GetService<UserController>()!;

        Assert.AreEqual(0, application.Services.GetService<UserRepository>()!.All().Count());
        ActionResult<bool> response = controller.CreateUser(new CreateUserPayload
        { 
            Mail = "remi@atypikhouse.fr",
            Password = "1234Azerty___",
            Phone = "0612234556", 
            FirstName = "Rémi",
            LastName = "Schneider"
        });

        Assert.IsTrue(response.Value);
        Assert.AreEqual(1, application.Services.GetService<UserRepository>()!.All().Count());
    }
    [TestMethod]
    public void CreatBadUser()
    {
        WebApplication application = TestsUtils.CreateContext();
        UserController controller = application.Services.GetService<UserController>()!;

        Assert.AreEqual(0, application.Services.GetService<UserRepository>()!.All().Count());
        ActionResult<bool> response = controller.CreateUser(new CreateUserPayload
        {
            Mail = "remi.fr",
            Password = "1234azerty___",
            Phone = "0612234556",
            FirstName = "Rémi",
            LastName = "Schneider"
        });

        Assert.IsFalse(response.Value);
        Assert.AreEqual(0, application.Services.GetService<UserRepository>()!.All().Count());
    }
}