using Autofac;
using AutoMapper;
using FluentValidation;
using Webapi.Model;
using Webapi.Service;
using Webapi.Validator;

namespace Webapi.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
            builder.Register(ctx =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });

                return config.CreateMapper(); // returns IMapper
            })
            .As<IMapper>()
            .SingleInstance();

            builder.RegisterType<TodoService>()
                  .As<ITodoService>()
                  .InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>()
                   .As<ICategoryService>()
                   .InstancePerLifetimeScope();


            builder.RegisterType<TodoItemDtoValidator>()
                   .As<IValidator<TodoItemDto>>()
                   .InstancePerLifetimeScope();
        }
    }
}
