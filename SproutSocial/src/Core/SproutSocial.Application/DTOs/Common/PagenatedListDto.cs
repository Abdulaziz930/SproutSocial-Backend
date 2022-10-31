namespace SproutSocial.Application.DTOs.Common;

public class PagenatedListDto<T> where T : class
{
    public IEnumerable<T> Items { get; } = null!;
    public int PageIndex { get; }
    public int TotalPages { get; }
    public bool HasNext { get; }
    public bool HasPrev { get; }

    public PagenatedListDto(IEnumerable<T> items, int totalCount, int pageIndex, int pageSize)
    {
        Items = items;
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        HasNext = pageIndex < TotalPages;
        HasPrev = pageIndex > 1;
    }
}
