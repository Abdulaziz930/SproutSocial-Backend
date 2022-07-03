using SproutSocial.Service.Dtos;
using SproutSocial.Service.Dtos.TopicDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Services.Interfaces
{
    public interface ITopicService
    {
        Task<TopicDetailDto> GetByIdAsync(int? id);
        Task<List<TopicListItemDto>> GetAllAsync();
        Task<PagenatedListDto<TopicListItemDto>> GetPagenatedAsync(int? page);
        Task<bool> IsExistsByIdAsync(int? id);
        Task CreateAsync(TopicPostDto topicDto);
        Task EditAsync(int? id, TopicPostDto topicDto);
        Task DeleteAsync(int? id);
    }
}
