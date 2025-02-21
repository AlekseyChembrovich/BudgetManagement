using AutoMapper;
using BudgetManagement.Core.Entities;
using BudgetManagement.Server.Dtos.Category;

namespace BudgetManagement.Server.Mapping;

internal sealed class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<ExpenseCategory, CategoryDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.RootId, opt => opt.MapFrom(src => src.RootId));
    }
}
