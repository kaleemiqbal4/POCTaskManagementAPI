using AutoMapper;
using POCProject.Entities.Entities;
using POCProject.Models.Request;

namespace POCProject.Services.DtoMapperProfile;

/// <summary>
/// AutoMapper profile for mapping between task models and entities.
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<TasksModel, TasksEntity>().ReverseMap();
        CreateMap<TaskColumnModel, TaskColumnEntity>().ReverseMap();
    }
}
