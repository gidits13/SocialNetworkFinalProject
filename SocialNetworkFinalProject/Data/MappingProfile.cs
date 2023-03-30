using AutoMapper;
using SocialNetworkFinalProject.Models;
using SocialNetworkFinalProject.ViewModels;
using SocialNetworkFinalProject.ViewModels.Account;
using System;

namespace SocialNetworkFinalProject.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.BirthDate, opt => opt.MapFrom(c => new DateTime((int)c.Year, (int)c.Month, (int)c.Date)))
                .ForMember(x=>x.UserName, opt=>opt.MapFrom(c=>c.Login));
            CreateMap<LoginViewModel, User>();

            CreateMap<User, UserEditViewModel>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(c => c.Id));

            CreateMap<User, UserWithFriendExt>();
        }
    }
}
