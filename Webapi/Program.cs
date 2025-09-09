using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Webapi.AutoMapper;
using Webapi.Data;
using Webapi.Service;
using Webapi.Validator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add DbContext with MySQL

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
     .UseLazyLoadingProxies()
    );



// Register Service
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TodoItemDtoValidator>());

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
