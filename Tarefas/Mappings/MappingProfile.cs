using AutoMapper;
using Tarefas.DTOs;
using Tarefas.Models;

namespace Tarefas.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // TAREFA
        CreateMap<Tarefa, TarefaPatchDTO>()
            .ForMember(dest => dest.Responsaveis, opt => opt.MapFrom(src => src.Responsaveis));

        CreateMap<TarefaPatchDTO, Tarefa>()
            .ForMember(dest => dest.Responsaveis, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Titulo, opt => opt.Condition(src => src.Titulo != null))
            .ForMember(dest => dest.Descricao, opt => opt.Condition(src => src.Descricao != null));

        CreateMap<Tarefa, TarefaDTO>()
            .ForMember(dest => dest.Responsaveis, opt => opt.MapFrom(src => src.Responsaveis));
        
        CreateMap<TarefaDTO, Tarefa>()
            .ForMember(dest => dest.Responsaveis, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        // PESSOA
        CreateMap<Pessoa, PessoaDTO>();

        CreateMap<PessoaDTO, Pessoa>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
