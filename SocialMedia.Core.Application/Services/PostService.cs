using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialMedia.Core.Application.Dtos.Account;
using SocialMedia.Core.Application.Helpers;
using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.ViewModels.Posts;
using SocialMedia.Core.Domain.Entities;

namespace SocialMedia.Core.Application.Services
{
    public class PostService : GenericService<SavePostViewModel,PostViewModel,Post>,IPostService
    {
        protected readonly IPostRepository _postRepository;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly AuthenticationResponse _userLoged;

        public PostService(IPostRepository _postRepository, IMapper _mapper, IHttpContextAccessor _httpContextAccessor) :base(_postRepository, _mapper)
        {
            this._postRepository = _postRepository;
            this._mapper = _mapper;
            _userLoged = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SavePostViewModel> AddAsync(SavePostViewModel postVm)
        {
            postVm.UserId = _userLoged.Id;
            return await base.AddAsync(postVm);
        }
    }
}
