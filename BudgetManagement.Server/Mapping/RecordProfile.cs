using AutoMapper;
using BudgetManagement.Core.Entities;
using BudgetManagement.Server.Dtos.Record;

namespace BudgetManagement.Server.Mapping;

internal sealed class RecordProfile : Profile
{
    public RecordProfile()
    {
        CreateMap<ExpenseRecord, RecordDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}
