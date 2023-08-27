using AtypikHouseBackEnd;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace UnitTests;

internal class TestsUtils
{
    public static WebApplication CreateContext()
    {
        WebApplication app = ProjectApplicationBuilder.CreateProjectApp(null, new Action<WebApplicationBuilder>[]
        {
            ProjectApplicationBuilder.MapControllers,
            i => i.Services.AddDbContext<AppDbContext, InMemoryAppDbContext>()
        });

        return (app);
    }
}