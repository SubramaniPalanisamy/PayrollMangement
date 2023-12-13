using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer;
using Microsoft.EntityFrameworkCore;
using complaintsWebApi.Models;
using complaintsWebApi.EFData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ComplaintsDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("connection1"))
);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
