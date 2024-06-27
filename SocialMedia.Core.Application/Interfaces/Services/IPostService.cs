using SocialMedia.Core.Application.ViewModels.Posts;
using SocialMedia.Core.Domain.Entities;

namespace SocialMedia.Core.Application.Interfaces.Services
{
    public interface IPostService : IGenericService<SavePostViewModel,PostViewModel,Post>
    {

    }
}
