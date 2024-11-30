using Microsoft.EntityFrameworkCore;

namespace FirstMicroservice.Todos.WebAPI.Context;

public sealed class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    public DbSet<Models.Todo> Todos { get; set; } = default!;
}