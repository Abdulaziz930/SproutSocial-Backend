using SproutSocial.Application.DTOs.TopicDtos;

namespace SproutSocial.Application.Abstractions.Services;

public interface ITopicService
{
    Task<List<TopicDto>> GetAllTopicsAsync();
    Task<TopicDto> GetTopicByIdAsync(string id);
    Task<bool> CreateTopicAsync(CreateTopicDto topic);
    Task<bool> UpdateTopicAsync(string id,UpdateTopicDto topic);
    Task<bool> DeleteTopicAsync(string id); 
}
