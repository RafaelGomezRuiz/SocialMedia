using AutoMapper;
using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Core.Domain.Entities;
using SocialMedia.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Persistence.Repositories
{
    public class FriendRepository : GenericRepository<Friend>,IFriendRepository
    {
        protected readonly ApplicationContext _applicationContext;
        public FriendRepository(ApplicationContext _applicationContext, IMapper _mapper) : base(_applicationContext)
        {
            this._applicationContext = _applicationContext;
        }
    }
}
