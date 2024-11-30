using FirstMicroservice.Categories.WebAPI.Mdoel;
using Microsoft.EntityFrameworkCore;

namespace FirstMicroservice.Categories.WebAPI.Context;

public sealed class ApplicationContext: DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Category> Categories { get; set; }
}