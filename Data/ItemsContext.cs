using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NTier.Data
{
    public class ItemsContext : IdentityDbContext
    { 
        public ItemsContext(DbContextOptions<ItemsContext> options) : base(options)
        { 
        
        }
                        // Items is the table name
        public DbSet<ItemModel> Items { get; set; } = default;
    }
}
