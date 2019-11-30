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
    }

    public class PostRepository : IPostRepository
    {
        private readonly TemplateContext _context = null;

        public PostRepository(IOptions<Settings> settings)
        {
            _context = new TemplateContext(settings);
        }

        public async Task AddPost(Post item)
        {

            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async Task<Post> GetPost(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            throw new NotImplementedException();
        }

        public async Task<Post> GetPostByName(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            throw new NotImplementedException();
        }

        public async void RemovePost(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            throw new NotImplementedException();
        }

        public async void UpdatePost(string id, Post item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            throw new NotImplementedException();
        }
    }
}
