using SproutSocial.Application.DTOs.Common;

namespace SproutSocial.Application.Abstractions.Services;

public interface ITopicService
{
    Task<PagenatedListDto<TopicDto>> GetAllTopicsAsync(int page, string? search);
    Task<TopicDto> GetTopicByIdAsync(string id);
    Task<bool> CreateTopicAsync(CreateTopicDto topic);
    Task<bool> UpdateTopicAsync(string id,UpdateTopicDto topic);
    Task<bool> DeleteTopicAsync(string id); 
}
