using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kulkov.Repository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPost(string id);
        Task<Post> GetPostByName(string id);
        Task AddPost(Post item);
        void RemovePost(string id);
        // обновление содержания (body) записи
        void UpdatePost(string id, Post item);
        void UpdatePosts(string id, Post item);
    }

    public class PostRepository : IPostRepository
    {
        private readonly TemplateContext _context = null;

        public PostRepository(IOptions<Settings> settings)
        {
            _context = new TemplateContext(settings);
        }

        public Task AddPost(Post item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPost(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPostByName(string id)
        {
            throw new NotImplementedException();
        }

        public void RemovePost(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(string id, Post item)
        {
            throw new NotImplementedException();
        }

        public void UpdatePosts(string id, Post item)
        {
            throw new NotImplementedException();
        }
    }
}
