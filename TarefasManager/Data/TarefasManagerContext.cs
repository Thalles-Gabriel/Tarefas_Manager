using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using TarefasManager.Models;

namespace TarefasManager.Data;

public class TarefasManagerContext : DbContext
{
    private readonly IConfiguration _config;

    public TarefasManagerContext(DbContextOptions<TarefasManagerContext> options, IConfiguration configuration) : base(options)
    {
        _config = configuration;
    }

    public DbSet<Tarefa> Tarefas => Set<Tarefa>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tarefa>().HasKey(x => x.Id);

        modelBuilder.Entity<Tarefa>().Property(x => x.Titulo)
                                        .IsRequired(true)
                                        .HasMaxLength(100)
                                        .IsUnicode(true);

        modelBuilder.Entity<Tarefa>().Property(x => x.Id)
                                        .IsRequired()
                                        .IsConcurrencyToken()
                                        .ValueGeneratedOnAdd();

        modelBuilder.Entity<Tarefa>().Property(x => x.Descricao)
                                        .IsRequired(false)
                                        .HasMaxLength(400)
                                        .IsUnicode(true);

        modelBuilder.Entity<Tarefa>().ToCollection("tarefas");

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var client = new MongoClient(_config.GetConnectionString("mongodb"));
        optionsBuilder.UseMongoDB(client, "tarefasDB")
                        .LogTo(Console.WriteLine);
        
    }
}
