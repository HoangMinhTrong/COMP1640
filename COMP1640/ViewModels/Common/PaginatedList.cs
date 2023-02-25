namespace COMP1640.ViewModels.Common;

public class PaginatedList<T>:List<T>
{
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    
    public int TotalItems { get; private set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalItems = count;

        this.AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    // public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    // {
    //     var count = await source.CountAsync();
    //     var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
    //     return new PaginatedList<T>(items, count, pageIndex, pageSize);
    // }

    public static IQueryable<T>CreatePangingQueryAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        source =  source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return source.AsQueryable();
    }

    public static Task<PaginatedList<T>> GetPagingResult(List<T> source,int count, int pageIndex, int pageSize)
    {
        
        return Task.FromResult(new PaginatedList<T>(source, count, pageIndex, pageSize));
    }
}