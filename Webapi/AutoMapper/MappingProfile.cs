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
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Category, CategoryDto>();


            CreateMap<TodoItemDto, TodoItem>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
