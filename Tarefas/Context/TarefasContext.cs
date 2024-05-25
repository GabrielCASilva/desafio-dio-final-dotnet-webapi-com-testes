using Microsoft.EntityFrameworkCore;
using Tarefas.Models;

namespace Tarefas.Context;
public class TarefasContext(DbContextOptions<TarefasContext> options) : DbContext(options)
{
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tarefa>()
            .HasMany(t => t.Responsaveis)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "TarefaPessoa", // Nome da tabela intermediária
                j => j.HasOne<Pessoa>().WithMany().HasForeignKey("PessoaId"),
                j => j.HasOne<Tarefa>().WithMany().HasForeignKey("TarefaId")
            );

        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Tarefa>().HasData(
            new Tarefa
            {
                Id = 1,
                Titulo = "Tarefa 1",
                Descricao = "Descrição da tarefa 1",
                DataCriacao = DateTime.Now,
                Status = EnumStatusTarefas.Pendente,
            },
            new Tarefa
            {
                Id = 2,
                Titulo = "Tarefa 2",
                Descricao = "Descrição da tarefa 2",
                DataCriacao = DateTime.Now,
                Status = EnumStatusTarefas.Pendente,
            },
            new Tarefa
            {
                Id = 3,
                Titulo = "Tarefa 3",
                Descricao = "Descrição da tarefa 3",
                DataCriacao = DateTime.Now,
                Status = EnumStatusTarefas.Pendente,
            },
            new Tarefa
            {
                Id = 4,
                Titulo = "Tarefa 4",
                Descricao = "Descrição da tarefa 4",
                DataCriacao = DateTime.Now,
                Status = EnumStatusTarefas.Pendente,
            }
        );

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });
        
        modelBuilder.Entity<Pessoa>().HasData(
            new Pessoa
            {
                Id = 1,
                Nome = "Pedro Rodrigo da Silva",
            },
            new Pessoa
            {
                Id = 2,
                Nome = "Matheus Henrique da Silva",
            },
            new Pessoa
            {
                Id = 3,
                Nome = "Mariana Aparecida de Jesus",
            },
            new Pessoa
            {
                Id = 4,
                Nome = "Jessica Peixoto de Souza",
            }
        );
    }
}