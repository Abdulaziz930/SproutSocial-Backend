using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SproutSocial.Service.Dtos;
using SproutSocial.Service.Dtos.TopicDtos;
using SproutSocial.Service.Services.Interfaces;

namespace SproutSocial.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _topicService.GetByIdAsync(id));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(int? page = 1)
        {
            return Ok(await _topicService.GetPagenatedAsync(page));
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(TopicPostDto topicPostDto)
        {
            await _topicService.CreateAsync(topicPostDto);

            return StatusCode(StatusCodes.Status201Created, new ResponseDto
            {
                Status = StatusCodes.Status201Created,
                Message = "Topic successfully created"
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, TopicPostDto topicPostDto)
        {
            await _topicService.EditAsync(id, topicPostDto);

            return NoContent();
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _topicService.DeleteAsync(id);

            return Ok(new ResponseDto
            {
                Status = StatusCodes.Status200OK,
                Message = "Topic successfully deleted"
            });
        }
    }
}
