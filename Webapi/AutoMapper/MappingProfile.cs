using Webapi.Model;
using AutoMapper;
using Webapi.Model;

namespace Webapi.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, TodoItemDto>()
                           .ForMember(dest => dest.CategoryName,
                                      opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));

            // Category → CategoryDto
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Todos,
                           opt => opt.MapFrom(src => src.TodoItems)); // Map collection

            // Reverse mapping: DTO → Entity
            CreateMap<TodoItemDto, TodoItem>()
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // Avoid EF circular issues

            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.TodoItems, opt => opt.Ignore()); // Ignore collection to prevent accidental overwrite
        }
    }
}
