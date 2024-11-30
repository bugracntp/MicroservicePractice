using FirstMicroservice.Categories.WebAPI.Context;
using FirstMicroservice.Categories.WebAPI.DTO;
using FirstMicroservice.Categories.WebAPI.Mdoel;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("NpgsqlConnection")));
var app = builder.Build();

app.MapGet("/cateogry/GetAll", async (ApplicationContext context, CancellationToken cancellationToken) =>
{
    return await context.Categories.ToListAsync(cancellationToken);
});

app.MapPost("/category/add", async (CategoryDTO category, ApplicationContext context,  CancellationToken cancellationToken) =>
{
    var newCategory = new Category
    {
        Name = category.name
    };
    bool isExist = await context.Categories.AnyAsync(c => c.Name == newCategory.Name, cancellationToken);
    if (isExist)
    {
        return Results.BadRequest(new { message = "Category already exists" });
    }
    await context.Categories.AddAsync(newCategory, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);
    return Results.Ok(new { message = "Category added successfully" });
});

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    context.Database.Migrate();
}
{
    
}

app.Run();