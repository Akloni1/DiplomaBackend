using AutoMapper;
using Diploma.ViewModels.Boxers;
using Diploma.ViewModels.BoxingClubs;
using Diploma.ViewModels.Coaches;
using Diploma.ViewModels.Competitions;
using Diploma.ViewModels.CompetitionsBoxers;
using Diploma.ViewModels.CompetitionsClubs;

namespace Diploma.ViewModels.AutoMapperProfiles


{
    public class BoxerProfile : Profile
    {
        public BoxerProfile()
        {
            CreateMap<Diploma.Boxers, InputBoxerViewModel>().ReverseMap();
            CreateMap<Diploma.Boxers, DeleteBoxerViewModel>();
            CreateMap<Diploma.Boxers, EditBoxerViewModel>().ReverseMap();
            CreateMap<Diploma.Boxers, BoxerViewModel>();
            CreateMap<Diploma.BoxingClubs, InputBoxingClubsViewModel>().ReverseMap();
            CreateMap<Diploma.BoxingClubs, DeleteBoxingClubsViewModel>();
            CreateMap<Diploma.BoxingClubs, EditBoxingClubsViewModel>().ReverseMap();
            CreateMap<Diploma.BoxingClubs, BoxingClubsViewModel>();
            CreateMap<Diploma.Coaches, InputCoachViewModel>().ReverseMap();
            CreateMap<Diploma.Coaches, DeleteCoachViewModel>();
            CreateMap<Diploma.Coaches, EditCoachViewModel>().ReverseMap();
            CreateMap<Diploma.Coaches, CoachViewModel>();
            CreateMap<Diploma.Competitions, InputCompetitionsViewModel>().ReverseMap();
            CreateMap<Diploma.Competitions, DeleteCompetitionsViewModel>();
            CreateMap<Diploma.Competitions, EditCompetitionsViewModel>().ReverseMap();
            CreateMap<Diploma.Competitions, CompetitionsViewModel>();
            CreateMap<Diploma.CompetitionsBoxers, InputCompetitionsBoxersViewModel>().ReverseMap();
            CreateMap<Diploma.CompetitionsBoxers, DeleteCompetitionsBoxersViewModel>();
            CreateMap<Diploma.CompetitionsBoxers, EditCompetitionsBoxersViewModel>().ReverseMap();
            CreateMap<Diploma.CompetitionsBoxers, CompetitionsBoxersViewModel>();
            CreateMap<Diploma.CompetitionsClubs, InputCompetitionsClubsViewModel>().ReverseMap();
            CreateMap<Diploma.CompetitionsClubs, DeleteCompetitionsClubsViewModel>();
            CreateMap<Diploma.CompetitionsClubs, EditCompetitionsClubsViewModel>().ReverseMap();
            CreateMap<Diploma.CompetitionsClubs, CompetitionsClubsViewModel>();
        }
    }
}