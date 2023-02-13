namespace Templ.Application.Services;

public class CustomerQueryParams
{
    public int? Page { get; set; }
    
    public int? PageSize { get; set; }

    public string? OrderBy { get; set; }
    
    public bool? OrderByAsc { get; set; }

    public string[]? FilterBy { get; set; }

    public string[]? FilterByText { get; set; }
}
