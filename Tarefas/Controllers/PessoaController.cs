using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarefas.Context;
using Tarefas.DTOs;
using Tarefas.Models;

namespace Tarefas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController(TarefasContext context, IMapper mapper) : ControllerBase
{
    private readonly TarefasContext _context = context;
    private readonly IMapper _mapper = mapper;

    // GET: api/pessoa
    [HttpGet]
    public async Task<ActionResult<List<PessoaDTO>>> GetAll()
    {
        var pessoas = await _context.Pessoas.ToListAsync();
        var dtos = _mapper.Map<List<PessoaDTO>>(pessoas);
        return dtos;
    }

    // GET: api/pessoa/1
    [HttpGet("{id}")]
    public async Task<ActionResult<PessoaDTO>> GetById(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        
        if (pessoa == null)
        {
            return NotFound("Pessoa não encontrada.");
        }
        
        var dto = _mapper.Map<PessoaDTO>(pessoa);
        return dto;
    }

    // POST: api/pessoa
    [HttpPost]
    public async Task<ActionResult<PessoaDTO>> Create([FromBody] PessoaDTO pessoaDto)
    {
        if (pessoaDto == null)
        {
            return BadRequest("Dados inválidos.");
        }
        
        var pessoa = _mapper.Map<Pessoa>(pessoaDto);
        
        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();
        
        var dto = _mapper.Map<PessoaDTO>(pessoa);
        return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, dto);
    }

    // PUT: api/pessoa/1
    [HttpPut("{id}")]
    public async Task<ActionResult<PessoaDTO>> Update(int id, [FromBody] PessoaDTO pessoaDto)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        
        if (pessoa == null)
        {
            return NotFound(new { message = "Pessoa não encontrada" });
        }
        
        _mapper.Map(pessoaDto, pessoa);
        await _context.SaveChangesAsync();
        
        var dto = _mapper.Map<PessoaDTO>(pessoa);
        return dto;
    }

    // DELETE: api/pessoa/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);

        if (pessoa == null)
        {
            return NotFound("Pessoa não encontrada.");
        }

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}