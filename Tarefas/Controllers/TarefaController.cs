using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarefas.Context;
using Tarefas.DTOs;
using Tarefas.Models;

namespace Tarefas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefaController(TarefasContext context, IMapper mapper) : ControllerBase
{
    private readonly TarefasContext _context = context;
    private readonly IMapper _mapper = mapper;

    // GET: api/tarefa
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetAll()
    {
        var tarefas = await _context.Tarefas.Include(t => t.Responsaveis).ToListAsync();
        var dtos = _mapper.Map<List<TarefaDTO>>(tarefas);
        return dtos;
    }

    // GET: api/tarefa/1
    [HttpGet("{id}")]
    public async Task<ActionResult<TarefaDTO>> GetById(int id)
    {
        var tarefa = await _context.Tarefas.Include(t => t.Responsaveis).FirstOrDefaultAsync(t => t.Id == id);

        if (tarefa == null)
        {
            return NotFound(new { message = "Tarefa não encontrada" });
        }

        var dto = _mapper.Map<TarefaDTO>(tarefa);
        return dto;
    }

    // POST: api/tarefa
    [HttpPost]
    public async Task<ActionResult<TarefaDTO>> Create([FromBody] TarefaDTO tarefaDto)
    {
        if(tarefaDto == null)
        {
            return BadRequest("Dados inválidos.");
        }

        var tarefa = _mapper.Map<Tarefa>(tarefaDto);

        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        var dto = _mapper.Map<TarefaDTO>(tarefa);
        return CreatedAtAction(nameof(GetById), new { id = tarefa.Id }, dto);
    }

    // PATCH: api/tarefa/1
    [HttpPatch("{id}")]
    public async Task<ActionResult<TarefaDTO>> Update(int id, [FromBody] TarefaPatchDTO tarefaDto)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);

        if(tarefa == null)
        {
            return NotFound(new { message = "Tarefa não encontrada" });
        }

        _mapper.Map(tarefaDto, tarefa);
        await _context.SaveChangesAsync();

        var dto = _mapper.Map<TarefaDTO>(tarefa);
        return dto;
    }

    // PATCH: api/tarefa/1/status
    [HttpPatch("{id}/status")]
    public async Task<ActionResult<TarefaDTO>> UpdateStatus(int id, [FromBody] EnumStatusTarefas status)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);

        if(tarefa == null)
        {
            return NotFound(new { message = "Tarefa não encontrada" });
        }

        tarefa.Status = status;

        if (status == EnumStatusTarefas.EmAndamento)
        {
            tarefa.DataInicio = DateTime.Now;
        }
        else if (status == EnumStatusTarefas.Concluida)
        {
            tarefa.DataConclusao = DateTime.Now;

            if(tarefa.DataInicio == null)
            {
                tarefa.DataInicio = DateTime.Now;
            }
        }

        await _context.SaveChangesAsync();

        var dto = _mapper.Map<TarefaDTO>(tarefa);
        return dto;
    }

    // DELETE: api/tarefa/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);

        if(tarefa == null)
        {
            return NotFound(new { message = "Tarefa não encontrada" });
        }

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/tarefa/responsavel
    [HttpPost("responsavel")]
    public async Task<ActionResult<TarefaDTO>> AddResponsavel([FromBody] TarefaPessoaDTO dto)
    {
        var tarefa = await _context.Tarefas.Include(t => t.Responsaveis).FirstOrDefaultAsync(t => t.Id == dto.TarefaId);
        
        if (tarefa == null)
        {
            return NotFound(new { message = "Tarefa não encontrada" });
        }

        var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId);
        if (pessoa == null)
        {
            return NotFound(new { message = "Pessoa não encontrada" });
        }

        if (tarefa.Responsaveis != null && tarefa.Responsaveis.Any(p => p.Id == pessoa.Id))
        {
            return BadRequest(new { message = "Pessoa já é responsável pela tarefa" });
        }

        tarefa.Responsaveis ??= [];

        tarefa.Responsaveis.Add(pessoa);
        await _context.SaveChangesAsync();

        var tarefaDto = _mapper.Map<TarefaDTO>(tarefa);
        return tarefaDto;
    }

    // DELETE: api/tarefa/responsavel
    [HttpDelete("responsavel")]
    public async Task<ActionResult<TarefaDTO>> RemoveResponsavel([FromBody] TarefaPessoaDTO dto)
    {
        var tarefa = await _context.Tarefas.Include(t => t.Responsaveis).FirstOrDefaultAsync(t => t.Id == dto.TarefaId);
        if (tarefa == null)
        {
            return NotFound(new { message = "Tarefa não encontrada" });
        }

        var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId);
        if (pessoa == null)
        {
            return NotFound(new { message = "Pessoa não encontrada" });
        }

        tarefa.Responsaveis ??= [];

        tarefa.Responsaveis.Remove(pessoa);
        await _context.SaveChangesAsync();

        var tarefaDto = _mapper.Map<TarefaDTO>(tarefa);
        return tarefaDto;
    }
}