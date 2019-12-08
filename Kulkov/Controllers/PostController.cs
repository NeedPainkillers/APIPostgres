using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulkov.Repository;
using Kulkov.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kulkov.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET api/Salary
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public Task<IEnumerable<Post>> Get()
        {
            return GetPostsInternal();
        }

        private async Task<IEnumerable<Post>> GetPostsInternal()
        {

            return await _postRepository.GetAllPosts();
        }

        // GET api/Salary/5
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("{id}")]
        public Task<Post> Get(string id)
        {
            return GetSalaryIdInternal(id);

        }

        private async Task<Post> GetSalaryIdInternal(string id)
        {
            return await _postRepository.GetPost(id) ?? new Post();
        }


        // POST api/Salary
        [HttpPost]
        public async Task<bool> Post([FromBody] Post item)
        {
            await _postRepository.AddPost(item);

            return true;
        }

        // PUT api/Salary/5
        [HttpPut("{id}")]
        public bool Put(string id, [FromBody] Post item)
        {
            _postRepository.UpdatePost(id, item);

            return true;
        }

        // DELETE api/Salary/23243423
        [HttpDelete("{id}")]
        public bool Delete(string id, [FromBody] string senderId)
        {
            _postRepository.RemovePost(id);

            return true;
        }
    }
}