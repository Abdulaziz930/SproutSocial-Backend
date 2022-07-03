using AutoMapper;
using SproutSocial.Core;
using SproutSocial.Core.Entities;
using SproutSocial.Service.Dtos;
using SproutSocial.Service.Dtos.TopicDtos;
using SproutSocial.Service.Exceptions;
using SproutSocial.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Services.Implementations
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TopicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(TopicPostDto topicDto)
        {
            if (await _unitOfWork.TopicRepository.IsExistsAsync(t => t.Name!.ToLower().Trim() == topicDto.Name!.ToLower().Trim() && !t.IsDeleted))
                throw new RecordAlreadyExistException("Topic name already exist");

            Topic topic = _mapper.Map<Topic>(topicDto);

            topic.CreatedAt = DateTime.UtcNow;
            topic.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.TopicRepository.InsertAsync(topic);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            if (id is null)
                throw new ArgumentNullException("Id cannot be null");

            Topic topic = await _unitOfWork.TopicRepository.GetAsync(t => t.Id == id && !t.IsDeleted);
            if (topic is null)
                throw new ItemNotFoundException($"Topic not found by id: {id}");

            topic.IsDeleted = true;

            await _unitOfWork.CommitAsync();    
        }

        public async Task EditAsync(int? id, TopicPostDto topicDto)
        {
            if (id is null)
                throw new ArgumentNullException("Id cannot be null");

            Topic topic = await _unitOfWork.TopicRepository.GetAsync(t => t.Id == id && !t.IsDeleted);
            if (topic is null)
                throw new ItemNotFoundException($"Topic not found by id: {id}");

            if (await _unitOfWork.TopicRepository.IsExistsAsync(t => t.Id != id && t.Name!.ToLower().Trim() == topicDto.Name!.ToLower().Trim() && !t.IsDeleted))
                throw new RecordAlreadyExistException("Topic name already exist");

            topic.Name = topicDto.Name;
            topic.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<TopicListItemDto>> GetAllAsync()
        {
            var topics = await _unitOfWork.TopicRepository.GetAllAsync(t => !t.IsDeleted);
            if(topics is null)
                throw new ItemNotFoundException("There is no any topic data");

            List<TopicListItemDto> topicsDto = _mapper.Map<List<TopicListItemDto>>(topics);
            return topicsDto;
        }

        public async Task<TopicDetailDto> GetByIdAsync(int? id)
        {
            if (id is null)
                throw new ArgumentNullException("Id cannot be null");

            Topic topic = await _unitOfWork.TopicRepository.GetAsync(t => t.Id == id && !t.IsDeleted);
            if (topic is null)
                throw new ItemNotFoundException($"Topic not found by id: {id}");

            TopicDetailDto topicDto = _mapper.Map<TopicDetailDto>(topic);
            return topicDto;
        }

        public async Task<PagenatedListDto<TopicListItemDto>> GetPagenatedAsync(int? page)
        {
            if (page is null)
                throw new ArgumentNullException("Id cannot be null");

            if (page < 1)
                throw new PageIndexFormatException("PageIndex must be greater or equal than 1");

            var items = await _unitOfWork.TopicRepository.GetAllPagenatedAsync(page.Value, 5, t => !t.IsDeleted);
            if (items is null)
                throw new ItemNotFoundException("There is no any topic data");

            int totalCount = await _unitOfWork.TopicRepository.GetTotalCountAsync(t => !t.IsDeleted);

            IEnumerable<TopicListItemDto> itemDtos = _mapper.Map<IEnumerable<TopicListItemDto>>(items);

            PagenatedListDto<TopicListItemDto> model = new PagenatedListDto<TopicListItemDto>(itemDtos, totalCount, page.Value, 5);
            return model;
        }

        public async Task<bool> IsExistsByIdAsync(int? id)
        {
            if (id is null)
                throw new ArgumentNullException("Id cannot be null");

            return await _unitOfWork.TopicRepository.IsExistsAsync(t => t.Id == id && !t.IsDeleted);
        }
    }
}
