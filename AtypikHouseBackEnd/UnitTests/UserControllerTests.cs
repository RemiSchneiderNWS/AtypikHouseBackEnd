using AtypikHouseBackEnd;
using AtypikHouseBackEnd.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Payloads;

namespace UnitTests;

[TestClass]
public class UserControllerTests
{
    [TestMethod]
    public void CreateUser()
    {
        WebApplication application = TestsUtils.CreateContext();
        UserController controller = application.Services.GetService<UserController>()!;

        ActionResult<bool> response = controller.CreateUser(new CreateUserPayload
        { 
            Mail = "remi@atypikhouse.fr",
            Password = "1234Azerty___",
            Phone = "0612234556", 
            FirstName = "Rémi",
            LastName = "Scheinder"
        });

        Assert.IsTrue(response.Value);
    }
}