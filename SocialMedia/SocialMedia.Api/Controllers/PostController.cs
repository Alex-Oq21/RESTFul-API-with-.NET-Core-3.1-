using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public  async Task <IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            var PostsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);
           
            return Ok(PostsDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);
            var postDTO = _mapper.Map<PostDTO>(post);
            return Ok(post);
        }
        [HttpPost]
        public async Task<IActionResult>Post(PostDTO postDTO)
        {
            if (! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var post = _mapper.Map<Post>(postDTO);
            await _postRepository.InsertPost(post);
            return Ok(post);
        }
    }
}
