using BusinessLogic.Utils;
using DataAccess.Repositories.PostRepo;
using DataAccess.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services.PostService
{
    public class PostService : IPostService
    {
        private IPostRepo _postRepo;
        private DecodeToken _decodeToken;

        public PostService(IPostRepo postRepo)
        {
            _postRepo = postRepo;
            _decodeToken = new DecodeToken();
        }

        public async Task<ViewPost> GetPostByIdAsync(int postId)
        {
            var post = await _postRepo.GetPostById(postId);
            if (post == null) throw new NullReferenceException("Not found any posts!");
            return post;
        }

        public async Task CreatePostAsync(string token, PostFormModel model)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("Admin")) throw new UnauthorizedAccessException("You do not have permission to do this action!");
                int userId = _decodeToken.Decode(token, "UserId");
                await _postRepo.CreatePost(model, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CanCreateAsync(string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("Admin")) throw new UnauthorizedAccessException("You do not have permission to view this resource!");
            int userId = _decodeToken.Decode(token, "UserId");
            var posts = await _postRepo.GetPostsByUserId(userId);
            var canCreate = true;
            if (posts == null) return canCreate;
            if(posts.Count >= 1 && role.Equals("User")) canCreate = false;
            return canCreate;
        }

        public async Task ApprovePostAsync(string token, int postId)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("User") || role.Equals("Vip")) 
                    throw new UnauthorizedAccessException("You do not have permission to do this action!");
                var post = await _postRepo.GetPostById(postId);
                if (post == null) throw new NullReferenceException("Not found any posts!");
                await _postRepo.ApprovePost(postId);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<ViewPost>> GetAllPostsAsync(PostRequestModel request)
        {
            var posts = await _postRepo.GetAllPosts(request);
            if (posts == null) throw new NullReferenceException("Not found any posts!");
            return posts;
        }

        public async Task DisablePostAsync(int postId)
        {
            try
            {
                var post = await _postRepo.GetPostById(postId);
                if (post == null) throw new NullReferenceException("Not found any posts!");
                await _postRepo.DisablePost(postId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EditPostAsync(string token, PostFormModel model, int postId)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("Admin")) throw new UnauthorizedAccessException("You do not have permission to do this action!");
                var post = await _postRepo.GetPostById(postId);
                if (post == null) throw new NullReferenceException("Not found any posts!");
                await _postRepo.EditPost(model, postId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<ViewPost>> GetPersonalPostsAsync(int userId)
        {
            var posts = await _postRepo.GetPersonalPosts(userId);
            if (posts == null) throw new NullReferenceException("Not found any posts!");
            return posts;
        }
    }
}
