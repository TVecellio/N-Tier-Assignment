using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace NTier.Data
{
    public class ItemsContext : DbContext
    { 
        public ItemsContext(DbContextOptions<ItemsContext> options) : base(options)
        { 
        
        }
                        // Items is the table name
        public DbSet<ItemModel> Items { get; set; } = default;
    }
}
