using BusinessObject.Context;
using BusinessObject.Models;
using DataAccess.Repositories.GenericRepo;
using DataAccess.ViewModels.Posts;
using System.Threading.Tasks;
using System.Linq;
using DataAccess.Enum;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccess.Repositories.PostRepo
{
    public class PostRepo : GenericRepo<Post>, IPostRepo
    {
        public PostRepo(TroTotDBContext context) : base(context)
        {
        }

        public async Task<ViewPost> GetPostById(int postId)
        {
            var query = from p in context.Posts join c in context.Categories on p.CategoryId equals c.CategoryId
                        join t in context.Types on p.TypeId equals t.TypeId
                        join u in context.Users on p.UserId equals u.UserId
                        where p.PostId.Equals(postId)
                        select new { p, c, t, u };
            ViewPost post = await query.Select(selector => new ViewPost
            {
                PostId = selector.p.PostId,
                Title = selector.p.Title,
                Description = selector.p.Description,
                Image = selector.p.Image,
                Address = selector.p.Address,
                Price = selector.p.Price,
                DateTime = selector.p.DateTime,
                DateTimeString = selector.p.DateTime.ToString(),
                Baths= selector.p.Baths,
                Area = selector.p.Area,
                Beds = selector.p.Beds,
                Status = (PostStatus)selector.p.Status,
                StatusString = ((PostStatus)selector.p.Status).ToString(),
                NumberOfPeople = selector.p.NumberOfPeople,
                Wifi = selector.p.Wifi,
                CategoryId = selector.p.CategoryId,
                CategoryName = selector.c.CategoryName,
                TypeId = selector.p.TypeId,
                TypeName = selector.t.TypeName,
                UserId = selector.p.UserId,
                UserName = selector.u.FullName,
                UserImage = selector.u.Image
            }).FirstOrDefaultAsync();
            return (post != null) ? post : null;
        }

        public async Task CreatePost(PostFormModel model, int userId)
        {
            var post = new Post()
            {
                Title = model.Title,
                Description = model.Description,
                Image = model.Image,
                Address = model.Address,
                Price = model.Price,
                DateTime = System.DateTime.Now.ToLocalTime(),
                Area = model.Area,
                Baths = model.Baths,
                Beds = model.Beds,
                Status = (int) PostStatus.Pending,
                NumberOfPeople = model.NumberOfPeople,
                Wifi = model.Wifi,
                CategoryId = model.CategoryId,
                TypeId = model.TypeId,
                UserId = userId
            };
            await CreateAsync(post);
        }

        public async Task<IList<ViewPost>> GetPostsByUserId(int userId)
        {
            var query = from p in context.Posts
                        join c in context.Categories on p.CategoryId equals c.CategoryId
                        join t in context.Types on p.TypeId equals t.TypeId
                        join u in context.Users on p.UserId equals u.UserId
                        where p.UserId.Equals(userId)
                        select new { p, c, t, u };
            IList<ViewPost> items = await query.Select(selector => new ViewPost
            {
                PostId = selector.p.PostId,
                Title = selector.p.Title,
                Description = selector.p.Description,
                Image = selector.p.Image,
                Address = selector.p.Address,
                Price = selector.p.Price,
                DateTime = selector.p.DateTime,
                DateTimeString = selector.p.DateTime.ToString(),
                Baths = selector.p.Baths,
                Area = selector.p.Area,
                Beds = selector.p.Beds,
                Status = (PostStatus)selector.p.Status,
                StatusString = ((PostStatus)selector.p.Status).ToString(),
                NumberOfPeople = selector.p.NumberOfPeople,
                Wifi = selector.p.Wifi,
                CategoryId = selector.p.CategoryId,
                CategoryName = selector.c.CategoryName,
                TypeId = selector.p.TypeId,
                TypeName = selector.t.TypeName,
                UserId = selector.p.UserId,
                UserName = selector.u.FullName,
                UserImage = selector.u.Image
            }).ToListAsync();
            return (items.Count > 0) ? items : null;   
        }

        public async Task ApprovePost(int postId)
        {
            var post = await context.Posts.FirstOrDefaultAsync(p => p.PostId.Equals(postId));
            post.Status = (int)PostStatus.Active;
            await UpdateAsync(post);
        }

        public async Task<IList<ViewPost>> GetAllPosts(PostRequestModel request)
        {
            var query = from p in context.Posts
                        join c in context.Categories on p.CategoryId equals c.CategoryId
                        join t in context.Types on p.TypeId equals t.TypeId
                        join u in context.Users on p.UserId equals u.UserId
                        where p.Status.Equals((int) request.Status)
                        select new { p, c, t, u };
            if (!string.IsNullOrEmpty(request.Address))
                query = query.Where(selector => selector.p.Address.Contains(request.Address));
            if (request.MinPrice.HasValue && request.MaxPrice.HasValue) 
                query = query.Where(selector => selector.p.Price >= request.MinPrice && selector.p.Price <= request.MaxPrice);
            if (request.People.HasValue) query = query.Where(selector => selector.p.NumberOfPeople.Equals(request.People));
            IList<ViewPost> items = await query.OrderByDescending(selector => selector.u.RoleId).Select(selector => new ViewPost
            {
                PostId = selector.p.PostId,
                Title = selector.p.Title,
                Description = selector.p.Description,
                Image = selector.p.Image,
                Address = selector.p.Address,
                Price = selector.p.Price,
                DateTime = selector.p.DateTime,
                DateTimeString = selector.p.DateTime.ToString(),
                Baths = selector.p.Baths,
                Area = selector.p.Area,
                Beds = selector.p.Beds,
                Status = (PostStatus)selector.p.Status,
                StatusString = ((PostStatus)selector.p.Status).ToString(),
                NumberOfPeople = selector.p.NumberOfPeople,
                Wifi = selector.p.Wifi,
                CategoryId = selector.p.CategoryId,
                CategoryName = selector.c.CategoryName,
                TypeId = selector.p.TypeId,
                TypeName = selector.t.TypeName,
                UserId = selector.p.UserId,
                UserName = selector.u.FullName,
                UserImage = selector.u.Image
            }).ToListAsync();
            return (items.Count > 0) ? items : null;
        }

        public async Task DisablePost(int postId)
        {
            var post = await context.Posts.FirstOrDefaultAsync(p => p.PostId.Equals(postId));
            post.Status = (int)PostStatus.Disabled;
            await UpdateAsync(post);
        }

        public async Task EditPost(PostFormModel model, int postId)
        {
            var post = await context.Posts.FirstOrDefaultAsync(p => p.PostId.Equals(postId));
            post.Title = model.Title;
            post.Description = model.Description;
            post.Image =model.Image;
            post.Address = model.Address;
            post.Price = model.Price;
            post.DateTime = System.DateTime.Now.ToLocalTime();
            post.Area = model.Area;
            post.Baths = model.Baths;
            post.Beds = model.Beds;
            post.Status = (int)PostStatus.Pending;
            post.NumberOfPeople = model.NumberOfPeople;
            post.Wifi = model.Wifi;
            post.CategoryId = model.CategoryId;
            post.TypeId = model.TypeId;
            await UpdateAsync(post);
        }

        public async Task<IList<ViewPost>> GetPersonalPosts(int userId)
        {
            var query = from p in context.Posts
                        join c in context.Categories on p.CategoryId equals c.CategoryId
                        join t in context.Types on p.TypeId equals t.TypeId
                        join u in context.Users on p.UserId equals u.UserId
                        where p.UserId.Equals(userId) && p.Status == (int) PostStatus.Active
                        select new { p, c, t, u };
            IList<ViewPost> items = await query.Select(selector => new ViewPost
            {
                PostId = selector.p.PostId,
                Title = selector.p.Title,
                Description = selector.p.Description,
                Image = selector.p.Image,
                Address = selector.p.Address,
                Price = selector.p.Price,
                DateTime = selector.p.DateTime,
                DateTimeString = selector.p.DateTime.ToString(),
                Baths = selector.p.Baths,
                Area = selector.p.Area,
                Beds = selector.p.Beds,
                Status = (PostStatus)selector.p.Status,
                StatusString = ((PostStatus)selector.p.Status).ToString(),
                NumberOfPeople = selector.p.NumberOfPeople,
                Wifi = selector.p.Wifi,
                CategoryId = selector.p.CategoryId,
                CategoryName = selector.c.CategoryName,
                TypeId = selector.p.TypeId,
                TypeName = selector.t.TypeName,
                UserId = selector.p.UserId,
                UserName = selector.u.FullName,
                UserImage = selector.u.Image    
            }).ToListAsync();
            return (items.Count > 0) ? items : null;
        }
    }
}
