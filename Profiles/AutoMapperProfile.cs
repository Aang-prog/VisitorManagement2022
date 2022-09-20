using AutoMapper;
using VisitorManagement2022.Models;
using VisitorManagement2022.ViewModels;

namespace VisitorManagement2022.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Visitors, VisitorsVM>().ReverseMap();
            CreateMap<StaffNames, StaffNamesVM>().ReverseMap();
        }
    }
}
