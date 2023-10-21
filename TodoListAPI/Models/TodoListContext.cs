using Microsoft.EntityFrameworkCore;

namespace TodoListAPI.Models;

public partial class TodoListContext : DbContext
{
    public TodoListContext()
    {
    }

    public TodoListContext(DbContextOptions<TodoListContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(entity =>
        {
            // entity.HasKey(e => e.Id).HasName("PK__Todos__3214EC079F0C1B92");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Title).HasMaxLength(50);           
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
