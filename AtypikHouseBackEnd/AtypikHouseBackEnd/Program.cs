using System.Text;
using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();

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

var app = builder.Build();

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

app.Run();