using AutoMapper;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.DTOs.TopicDtos;
using SproutSocial.Application.Exceptions;

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
            throw new RecordAlreadyExistException("Topic name already exist");

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
            throw new NotFoundException($"Topic not found by id: {id}");

        topic.IsDeleted = true;
        
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async Task<List<TopicDto>> GetAllTopicsAsync()
    {
        var topics = await _unitOfWork.TopicReadRepository.GetFiltered(t => !t.IsDeleted, tracking: false).ToListAsync();
        if (topics is null)
            throw new NotFoundException("There is no any topic data");

        List<TopicDto> topicDtos = _mapper.Map<List<TopicDto>>(topics);
        return topicDtos;
    }

    public async Task<TopicDto> GetTopicByIdAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var topic = await _unitOfWork.TopicReadRepository.GetByIdAsync(id, tracking: false);
        if (topic is null)
            throw new NotFoundException($"Topic not found by id: {id}");

        TopicDto topicDto = _mapper.Map<TopicDto>(topic);
        return topicDto;
    }

    public async Task<bool> UpdateTopicAsync(string id, UpdateTopicDto topicDto)
    {
        ArgumentNullException.ThrowIfNull(id);

        var topic = await _unitOfWork.TopicReadRepository.GetByIdAsync(id);
        if (topic is null)
            throw new NotFoundException($"Topic not found by id: {id}");

        bool isExist = default;
        if(Guid.TryParse(id, out Guid topicId))
            isExist = await _unitOfWork.TopicReadRepository.IsExistsAsync(t => t.Name.ToLower().Trim() == topic.Name.ToLower().Trim() && !t.IsDeleted && t.Id != topicId);

        if(isExist)
            throw new RecordAlreadyExistException("Topic name already exist");

        topic.Name = topicDto.Name;
        _unitOfWork.TopicWriteRepository.Update(topic);

        await _unitOfWork.SaveAsync();

        return true;
    }
}
