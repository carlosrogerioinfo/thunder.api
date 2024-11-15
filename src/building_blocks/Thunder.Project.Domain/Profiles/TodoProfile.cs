using AutoMapper;
using Thunder.Project.Domain.Entities;
using Thunder.Project.Domain.Model.Response;
using Esterdigi.Core.Lib.Extensions;

namespace Thunder.Project.Domain.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<Todo, TodoResponse>()
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => (int)src.Status))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StringExtensionTools.GetDescriptionFromEnum(src.Status)))
                .ReverseMap();
        }
    }
}
