using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

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

 
    /*
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  
        #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//       // => optionsBuilder.UseSqlServer("Server=.;Database=TodoList;Trusted_Connection=True;TrustServerCertificate=True;");
{
    if (!optionsBuilder.IsConfigured)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // Set the base path
            .AddJsonFile("appsettings.json") // Use appsettings.json
            .Build();

        var connectionString = builder.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Todos__3214EC079F0C1B92");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
