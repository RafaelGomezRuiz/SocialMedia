using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialMedia.Core.Application.Dtos.Account;
using SocialMedia.Core.Application.Helpers;

using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.ViewModels.Friends;
using SocialMedia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Services
{
    public class FriendService :GenericService<SaveFriendViewModel, FriendViewModel, Friend>,IFriendService
    {
        protected readonly IFriendRepository _friendRepository;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContext;
        protected readonly AuthenticationResponse userLoged;


        public FriendService(IFriendRepository _friendRepository, IMapper _mapper, IHttpContextAccessor _httpContext) : base(_friendRepository, _mapper)
        {
            this._friendRepository = _friendRepository;
            this._mapper = _mapper;
            userLoged = _httpContext.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<IEnumerable<FriendViewModel>> GetAllFriendFromUser()
        {
            var friends = await _friendRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<FriendViewModel>>(friends.Where(friend=>
            friend.UserId==userLoged.Id));
            
        }
    }
}
