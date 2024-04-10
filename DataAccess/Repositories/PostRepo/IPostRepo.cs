using DataAccess.Enum;
using DataAccess.ViewModels.Posts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.PostRepo
{
    public interface IPostRepo
    {
        Task<ViewPost> GetPostById(int postId);
        Task CreatePost(PostFormModel model, int userId);
        Task<IList<ViewPost>> GetPostsByUserId(int userId);
        Task ApprovePost(int postId);
        Task<IList<ViewPost>> GetAllPosts(PostRequestModel request);
        Task DisablePost(int postId);
        Task EditPost(PostFormModel model, int postId);
        Task<IList<ViewPost>> GetPersonalPosts(int userId);
    }
}
