using DataAccess.ViewModels.Posts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services.PostService
{
    public interface IPostService
    {
        Task<ViewPost> GetPostByIdAsync(int postId);
        Task CreatePostAsync(string token, PostFormModel model);
        Task<bool> CanCreateAsync(string token);
        Task ApprovePostAsync(string token, int postId);
        Task<IList<ViewPost>> GetAllPostsAsync(PostRequestModel request);
        Task DisablePostAsync(int postId);
        Task EditPostAsync(string token, PostFormModel model, int postId);
        Task<IList<ViewPost>> GetPersonalPostsAsync(int userId);
    }
}
