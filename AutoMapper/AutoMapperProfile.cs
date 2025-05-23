﻿using AutoMapper;
using BirthdayApp.DTO;
using BirthdayApp.Model;

namespace BirthdayApp.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDTO,ApplicationUser>().ForMember(m=> m.UserName, opt=> opt.MapFrom(src=> src.Email));

            CreateMap<Friendship, FriendshipDTO>()
                .ForMember(dest => dest.RequesterName, option => option.MapFrom(x => x.Requester.Name))
                .ForMember(dest => dest.ReceiverName, option => option.MapFrom(x => x.Receiver.Name))
                .ReverseMap();


            CreateMap<WishList, WishListDTO> ().ReverseMap();
            CreateMap<UpdateWishListDTO, WishList>().ReverseMap();
            CreateMap<UpdateWishListDTO, WishListDTO>().ReverseMap();
            CreateMap<AddWishListDTO, WishList>().ReverseMap();

            
        }
    }
}
