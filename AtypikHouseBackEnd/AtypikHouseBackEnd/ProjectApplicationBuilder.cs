using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace AtypikHouseBackEnd;

public static class ProjectApplicationBuilder
{
    public static void BuildServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //builder.Services.AddDbContext<AppDbContext>();

        // Repositories
        builder.Services.AddScoped<ActivityRepository>();
        builder.Services.AddScoped<ActivityImageRepository>();
        builder.Services.AddScoped<AdvertImageRepository>();
        builder.Services.AddScoped<AdvertRepository>();
        builder.Services.AddScoped<CommentaryRepository>();
        builder.Services.AddScoped<CriteriaAdvertRepository>();
        builder.Services.AddScoped<CriteriaRepository>();
        builder.Services.AddScoped<ReservationRepository>();
        builder.Services.AddScoped<UserRepository>();

        // End Repositories

        builder.Services.AddCors(options =>
            options.AddDefaultPolicy(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["jwt:issuer"],
                    ValidAudience = builder.Configuration["jwt:issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
                };
            });
    }

    public static void ConfigureMiddleWares(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthorization();
        app.UseAuthorization();
        app.MapControllers();
    }

    public static WebApplication CreateProjectApp(string[]? args = null, Action<WebApplicationBuilder>[]? serviceOverloads = null)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args ?? Array.Empty<string>());
        // Add services to the container.

        ProjectApplicationBuilder.BuildServices(builder);

        foreach (Action<WebApplicationBuilder> action in (serviceOverloads ?? Array.Empty<Action<WebApplicationBuilder>>()))
        {
            action(builder);
        }

        var app = builder.Build();

        ProjectApplicationBuilder.ConfigureMiddleWares(app);
        
        return app;
    }

    public static void MapControllers(WebApplicationBuilder builder)
    {
        IEnumerable<Type> controllers = typeof(ProjectApplicationBuilder).Assembly
            .GetTypes()
            .Where(i => i.IsAssignableTo(typeof(Controller)));

        foreach (Type controller in controllers)
        {
            builder.Services.AddScoped(controller);
        }
    }
}