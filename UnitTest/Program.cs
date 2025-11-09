using Microsoft.EntityFrameworkCore;
using DataAccess.Services;
using DataAccess.Implements;
using Application.Services;
using Application.Implements;
using DomainModel.Models;
using Application.Implements.User;
using Application.Services.Commands.User;
using Application.Services.Queries.User;
using DataAccess.Implements.User;
using DataAccess.Services.Commands.User;
using DataAccess.Services.Queries;
using DomainModel.ModelsRead;
using Application;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// اتصال به دیتابیس
var strRead = builder.Configuration["ReadConnectionString"];
var strWite = builder.Configuration["WriteConnectionString"];
builder.Services.AddDbContext<Write_Context>(options =>
    options.UseSqlServer(strWite));

builder.Services.AddDbContext<Read_Context>(options =>
    options.UseSqlServer(strRead));
//use of version 13.1.0
//builder.Services.AddMediatR(cfg =>
//    cfg.RegisterServicesFromAssembly(typeof(ApplicationProjects).Assembly));
//use of verson 11.1.0
builder.Services.AddMediatR(typeof(ApplicationProjects).Assembly);



// ثبت سرویس‌های CQRS

// DAL
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();

// BLL
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware ها
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
