using AutoMapper;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.DTOs.Common;
using SproutSocial.Application.DTOs.TopicDtos;
using SproutSocial.Application.Exceptions.Common;
using SproutSocial.Application.Exceptions.Topics;

namespace SproutSocial.Persistence.Services;

public class TopicService : ITopicService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TopicService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> CreateTopicAsync(CreateTopicDto topic)
    {
        bool isExist = await _unitOfWork.TopicReadRepository.IsExistsAsync(t => t.Name.ToLower().Trim() == topic.Name.ToLower().Trim() && !t.IsDeleted);
        if(isExist) 
            throw new TopicAlreadyExistException();

        Topic newTopic = _mapper.Map<Topic>(topic);

        bool result = await _unitOfWork.TopicWriteRepository.AddAsync(newTopic);
        await _unitOfWork.SaveAsync();

        return result;
    }

    public async Task<bool> DeleteTopicAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);

        Topic topic = default;
        if (Guid.TryParse(id, out Guid topicId))
            topic = await _unitOfWork.TopicReadRepository.GetSingleAsync(t => t.Id == topicId && !t.IsDeleted);

        if (topic is null)
            throw new TopicNotFoundByIdException(Guid.Parse(id));

        topic.IsDeleted = true;
        
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async Task<PagenatedListDto<TopicDto>> GetAllTopicsAsync(int page, string? search)
    {
        if (page < 1) throw new PageFormatException();

        var topics = await _unitOfWork.TopicReadRepository.GetFiltered(t => !string.IsNullOrWhiteSpace(search) ? t.Name.ToLower().Contains(search.ToLower()) : true && !t.IsDeleted, page, 5,tracking: false).ToListAsync();
        if (topics is null)
            throw new TopicNotFoundException();

        var topicsCount = await _unitOfWork.TopicReadRepository.GetTotalCountAsync(t => !string.IsNullOrWhiteSpace(search) ? t.Name.ToLower().Contains(search.ToLower()) : true && !t.IsDeleted);

        IEnumerable<TopicDto> topicDtos = _mapper.Map<IEnumerable<TopicDto>>(topics);

        PagenatedListDto<TopicDto> pagenatedListDto = new(topicDtos, topicsCount, page, 5);

        return pagenatedListDto;
    }

    public async Task<TopicDto> GetTopicByIdAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var topic = await _unitOfWork.TopicReadRepository.GetByIdAsync(id, tracking: false);
        if (topic is null)
            throw new TopicNotFoundException();

        TopicDto topicDto = _mapper.Map<TopicDto>(topic);
        return topicDto;
    }

    public async Task<bool> UpdateTopicAsync(string id, UpdateTopicDto topicDto)
    {
        ArgumentNullException.ThrowIfNull(id);

        var topic = await _unitOfWork.TopicReadRepository.GetByIdAsync(id);
        if (topic is null)
            throw new TopicNotFoundByIdException(Guid.Parse(id));

        bool isExist = default;
        if(Guid.TryParse(id, out Guid topicId))
            isExist = await _unitOfWork.TopicReadRepository.IsExistsAsync(t => t.Name.ToLower().Trim() == topic.Name.ToLower().Trim() && !t.IsDeleted && t.Id != topicId);

        if(isExist) throw new TopicAlreadyExistException();

        topic.Name = topicDto.Name;
        _unitOfWork.TopicWriteRepository.Update(topic);

        await _unitOfWork.SaveAsync();

        return true;
    }
}
