using FirstMicroservice.Todos.WebAPI.Context;
using FirstMicroservice.Todos.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options=>
    options.UseInMemoryDatabase("Todos")
    );


var app = builder.Build();

app.MapPost("/todos/Add", (ApplicationDbContext context, string work) => 
{
    var todo = new Todo {Work = work}; 
    context.Todos.Add(todo); 
    context.SaveChanges(); 
    return new {Message = "Todo Added Successfully"};
});

app.MapGet("/todos/GetAll", (ApplicationDbContext context) => 
    {
    return context.Todos.ToListAsync();
    });

app.Run();