using System.Text;
using AtypikHouseBackEnd;
using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repositories;

WebApplication app = ProjectApplicationBuilder.CreateProjectApp(args, new Action<WebApplicationBuilder>[]
{
    i => i.Services.AddDbContext<AppDbContext, PGSQLAppDbContext>()
});

app.Run();