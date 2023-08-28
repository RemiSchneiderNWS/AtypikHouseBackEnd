using System.Text;
using AtypikHouseBackEnd;
using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories;

bool migration = false;

WebApplication app = ProjectApplicationBuilder.CreateProjectApp(args, new Action<WebApplicationBuilder>[]
{
    i => i.Services.AddScoped<AppDbContext, PGSQLAppDbContext>(o =>
    {
        PGSQLAppDbContext connection = new PGSQLAppDbContext(o.GetService<IConfiguration>()!["ConnectionStrings:Connection"] ?? null);

        if (!migration)
        {
            migration = true;
            connection.Database.Migrate();
        }

        return (connection);
    })
});

app.Run();