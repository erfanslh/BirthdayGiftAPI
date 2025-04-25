using AutoMapper;
using BirthdayApp.DTO;
using BirthdayApp.Model;

namespace BirthdayApp.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDTO,ApplicationUser>().ForMember(m=> m.UserName, opt=> opt.MapFrom(src=> src.Email));

            CreateMap<FriendshipDTO, Friendship>()
                .ForMember(destination => destination.Requester, opt => opt.MapFrom(src=> src.RequesterName))
                .ForMember(destination => destination.Receiver, opt => opt.MapFrom(src=> src.ReceiverName))
                .ReverseMap();
        }
    }
}
