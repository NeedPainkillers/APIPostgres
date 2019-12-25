using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kulkov.Repository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPost(int id);
        Task<Post> GetPostByName(int id);
        Task AddPost(Post item);
        Task RemovePost(int id);
        Task UpdatePost(int id, Post item);
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

            await using (var cmd = new NpgsqlCommand(string.Format("CALL insert_on_posts(text '{0}',(@date)::timestamp);", item.post_name), connection))
            {
                cmd.Parameters.AddWithValue("date", item.date_start);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            List<Post> Response = new List<Post>();
            await using (var cmd = new NpgsqlCommand("SELECT t.* FROM public.\"Posts\" t ORDER BY id_post ASC", connection))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    Response.Add(new Post()
                    {
                        id_post = Int32.Parse(reader.GetValue(0).ToString()),
                        post_name = reader.GetValue(1).ToString(),
                        date_start = reader.GetDateTime(2)
                    });
                }
            return Response;
        }

        public async Task<Post> GetPost(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using var cmd = new NpgsqlCommand(String.Format("SELECT t.* FROM public.\"Posts\" t WHERE t.id_post = {0}", id), connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            return new Post()
            {
                id_post = Int32.Parse(reader.GetValue(0).ToString()),
                post_name = reader.GetValue(1).ToString(),
                date_start = reader.GetDateTime(2)
            };
        }

        public async Task<Post> GetPostByName(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            throw new NotImplementedException();
        }

        public async Task RemovePost(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("CALL delete_on_posts((@id));", connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdatePost(int id, Post item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand(string.Format("CALL update_on_posts((@id), varchar '{0}', (@date)::timestamp);", item.post_name), connection))
            {
                cmd.Parameters.AddWithValue("id", item.id_post);
                cmd.Parameters.AddWithValue("date", item.date_start);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
