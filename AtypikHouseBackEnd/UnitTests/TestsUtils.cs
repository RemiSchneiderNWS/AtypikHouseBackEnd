using AtypikHouseBackEnd;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UnitTests;

internal static class TestsUtils
{
    public static WebApplication CreateContext()
    {
        WebApplication app = ProjectApplicationBuilder.CreateProjectApp(null, new Action<WebApplicationBuilder>[]
        {
            ProjectApplicationBuilder.MapControllers,
            i => i.Services.AddDbContext<AppDbContext, InMemoryAppDbContext>()
        });

        AppDbContext ctx = app.Services.GetService<AppDbContext>()!;

        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();
        return (app);
    }

    public static T Invoke<T, U, V>(this V ctrl, U payload, Func<V, U, T> method)
        where U : class
        where V : Controller
    {
        ctrl.ObjectValidator = new ObjectValidator();
        ctrl.TryValidateModel(payload);
        
        return (method(ctrl, payload));
    }
}