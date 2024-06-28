using AutoMapper;
using SocialMedia.Core.Application.Dtos.Account;
using SocialMedia.Core.Application.ViewModels.Friends;
using SocialMedia.Core.Application.ViewModels.Posts;
using SocialMedia.Core.Application.ViewModels.Users;
using SocialMedia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Mappings
{
    public class GeneralProfile :   Profile
    {
        public GeneralProfile() 
        {
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(destino => destino.HasError, otp => otp.Ignore())
                .ForMember(destino => destino.ErrorDescription, otp => otp.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(destino => destino.HasError, otp => otp.Ignore())
                .ForMember(destino => destino.ErrorDescription, otp => otp.Ignore())
                .ForMember(destino => destino.File, otp => otp.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(destino => destino.HasError, otp => otp.Ignore())
                .ForMember(destino => destino.ErrorDescription, otp => otp.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ResetPasswordViewModel>()
                .ForMember(destino => destino.HasError, otp => otp.Ignore())
                .ForMember(destino => destino.ErrorDescription, otp => otp.Ignore())
                .ReverseMap();

            CreateMap<Friend, FriendViewModel>()
                .ReverseMap()
                .ForMember(destino => destino.Created, otp => otp.Ignore())
                .ForMember(destino => destino.LastModified, otp => otp.Ignore())
                .ForMember(destino => destino.CreatedBy, otp => otp.Ignore())
                .ForMember(destino => destino.LastModifiedBy, otp => otp.Ignore());

            CreateMap<Friend, SaveFriendViewModel>()
                .ForMember(destino => destino.FriendId, otp => otp.Ignore())
                .ReverseMap()
                .ForMember(destino => destino.Created, otp => otp.Ignore())
                .ForMember(destino => destino.LastModified, otp => otp.Ignore())
                .ForMember(destino => destino.CreatedBy, otp => otp.Ignore())
                .ForMember(destino => destino.LastModifiedBy, otp => otp.Ignore());

            CreateMap<Post, SavePostViewModel>()
    .ForMember(dest => dest.File, opt => opt.Ignore())
    .ReverseMap()
    .ForMember(dest => dest.Comments, opt => opt.Ignore())
    .ForMember(dest => dest.Created, opt => opt.Ignore())
    .ForMember(dest => dest.LastModified, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
    .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
    .ForMember(dest => dest.VisualContentPath, opt => opt.MapFrom(src => src.VisualContentPath));
            CreateMap<Post, PostViewModel>()
                .ReverseMap()
                .ForMember(destino => destino.Created, otp => otp.Ignore())
                .ForMember(destino => destino.LastModified, otp => otp.Ignore())
                .ForMember(destino => destino.CreatedBy, otp => otp.Ignore())
                .ForMember(destino => destino.LastModifiedBy, otp => otp.Ignore());



        }
    }
}
