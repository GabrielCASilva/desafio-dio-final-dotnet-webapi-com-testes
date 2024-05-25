namespace Tarefas.Models;

public class Tarefa
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public string? Descricao { get; set; }
    public EnumStatusTarefas Status { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataConclusao { get; set; }
    public List<Pessoa>? Responsaveis { get; set; } = new List<Pessoa>();
}