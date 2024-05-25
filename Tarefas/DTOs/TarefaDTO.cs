using Tarefas.Models;

namespace Tarefas.DTOs;

public class TarefaDTO
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public string? Descricao { get; set; }
    public EnumStatusTarefas Status { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataConclusao { get; set; }
    public List<Pessoa>? Responsaveis { get; set; } = [];
}

public class TarefaPatchDTO
{
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public EnumStatusTarefas? Status { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataConclusao { get; set; }
    public List<Pessoa>? Responsaveis { get; set; }
}