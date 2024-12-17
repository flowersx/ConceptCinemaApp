using AutoMapper;
using DataAccess;
using Models.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CinemaHall, CinemaHallViewModel>()
         .ForMember(dest => dest.Rows, opt => opt.Ignore())
         .ForMember(dest => dest.SeatsPerRow, opt => opt.Ignore());

        // Map CinemaHallViewModel to CinemaHall
        CreateMap<CinemaHallViewModel, CinemaHall>();

        // Map Seat to SeatViewModel
        CreateMap<Seat, SeatViewModel>();
        CreateMap<SeatViewModel, Seat>();
    }
}
