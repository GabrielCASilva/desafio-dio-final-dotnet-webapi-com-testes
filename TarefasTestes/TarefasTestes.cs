using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarefas.Context;
using Tarefas.Controllers;
using Tarefas.DTOs;
using Tarefas.Mappings;
using Tarefas.Models;

namespace TarefasTestes;

public class TarefasTestes
{
    private readonly IMapper _mapper;
    private readonly TarefasContext _context;
    private readonly TarefaController _tarefaController;
    private readonly PessoaController _pessoaController;

    public TarefasTestes()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
        _context = new TarefasContext(new DbContextOptionsBuilder<TarefasContext>().UseInMemoryDatabase("TarefasTestes").Options);
        _tarefaController = new TarefaController(context: _context, mapper: _mapper);
        _pessoaController = new PessoaController(context: _context, mapper: _mapper);
    }

    [Fact]
    public async Task DeveAdicionarNovaTarefa()
    {
        var novaTarefa = new TarefaDTO
        {
            Titulo = "Teste Tarefa",
            Descricao = "Descrição da Tarefa",
            Status = EnumStatusTarefas.Pendente,
        };
    
        var actionResult = await _tarefaController.Create(novaTarefa);
    
        var createdAtActionResult = actionResult.Result as CreatedAtActionResult;
        Assert.NotNull(createdAtActionResult);
    
        var dto = createdAtActionResult.Value as TarefaDTO;
        Assert.NotNull(dto);
    
        Assert.Equal(novaTarefa.Titulo, dto.Titulo);
        Assert.Equal(novaTarefa.Descricao, dto.Descricao);
        Assert.Equal(novaTarefa.Status, dto.Status);
    }

    [Fact]
    public async Task DeveRetornarTarefaPorId()
    {
        var tarefa = new TarefaDTO
        {
            Titulo = "Teste Tarefa",
            Descricao = "Descrição da Tarefa",
            Status = EnumStatusTarefas.Pendente,
        };

        var actionResult = await _tarefaController.Create(tarefa);
        var createdAtActionResult = actionResult.Result as CreatedAtActionResult;

        if (createdAtActionResult?.Value is TarefaDTO dto)
        {
            var actionResultGet = await _tarefaController.GetById(dto.Id);
            var dtoGet = actionResultGet.Value;

            if (dtoGet != null)
            {
                Assert.Equal(dto.Id, dtoGet.Id);
                Assert.Equal(dto.Titulo, dtoGet.Titulo);
                Assert.Equal(dto.Descricao, dtoGet.Descricao);
                Assert.Equal(dto.Status, dtoGet.Status);
            }
        }
    }

    [Fact]
    public async Task DeveAdicionarUmaNovaPessoa()
    {
        var novaPessoa = new PessoaDTO
        {
            Nome = "Teste Pessoa",
        };

        var actionResult = await _pessoaController.Create(novaPessoa);

        var createdAtActionResult = actionResult.Result as CreatedAtActionResult;
        Assert.NotNull(createdAtActionResult);

        var dto = createdAtActionResult.Value as PessoaDTO;
        Assert.NotNull(dto);

        Assert.Equal(novaPessoa.Nome, dto.Nome);
    }

    [Fact]
    public async Task DeveRetornarPessoaPorId()
    {
        var pessoa = new PessoaDTO
        {
            Nome = "Teste Pessoa",
        };

        var actionResult = await _pessoaController.Create(pessoa);
        var createdAtActionResult = actionResult.Result as CreatedAtActionResult;

        if (createdAtActionResult?.Value is PessoaDTO dto)
        {
            var actionResultGet = await _pessoaController.GetById(dto.Id);
            var dtoGet = actionResultGet.Value;

            if (dtoGet != null)
            {
                Assert.Equal(dto.Id, dtoGet.Id);
                Assert.Equal(dto.Nome, dtoGet.Nome);
            }
        }
    }

    [Fact]
    public async Task DeveDeletarPessoaPorId()
    {
        var pessoa = new PessoaDTO
        {
            Nome = "Teste Pessoa",
        };

        var actionResult = await _pessoaController.Create(pessoa);
        var createdAtActionResult = actionResult.Result as CreatedAtActionResult;

        if (createdAtActionResult?.Value is PessoaDTO dto)
        {
            var actionResultDelete = await _pessoaController.Delete(dto.Id);
            var dtoDelete = actionResultDelete as OkResult;

            if (dtoDelete != null)
            {
                var actionResultGet = await _pessoaController.GetById(dto.Id);
                var dtoGet = actionResultGet.Value;

                Assert.Null(dtoGet);
            }
        }
    }

}