using System.Text;
using JewelleryStore;
using JewelleryStore.Database;
using JewelleryStore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
var policyName = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Prevent JSON serialization omit
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
}
);
// Prevent error 500 when missing params in request body
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// CORS for frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
        builder =>
        {
            builder
                .WithOrigins("http://localhost:3000")
                .WithOrigins("http://localhost:3001")
                .WithMethods("GET")
                .WithMethods("POST")
                .WithMethods("PUT")
                .WithMethods("DELETE")
                .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Connect to DB
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<MyDbContext>(
    dbContextOptions =>
    dbContextOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyName);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

