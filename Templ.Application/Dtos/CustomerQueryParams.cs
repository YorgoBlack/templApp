using FluentMediator.Pipelines;

namespace Templ.Application.Dtos;

public enum OrderByField
{
    None, Name, CompanyName, Phone, Email
}

public class CustomerQueryParams
{
    public int Top { get; set; }

    public int PageSize { get; set; }

    public OrderByField OrderBy { get; set; }

    public bool OrderByDesc { get; set; }

    public string? FilterName { get; set; }

    public string? FilterCompanyName { get; set; }

    public string? FilterPhone { get; set; }

    public string? FilterEmail { get; set; }

    public (string,object) BuidSql()
    {
        return (
            $"Exec GetCustomers @top,@pageSize,@orderBy,@orderByDesc,@filterName,@filterCompanyName,@filterPhone,@filterEmail",
            new {
                top = Top == 0 ? 1 : Top,
                pageSize = PageSize,
                orderBy = (int)OrderBy,
                orderByDesc = OrderByDesc ? 1 : 0,
                filterName = string.IsNullOrWhiteSpace(FilterName) ? "_" : FilterName,
                filterCompanyName = string.IsNullOrWhiteSpace(FilterCompanyName) ? "_" : FilterCompanyName,
                filterPhone = string.IsNullOrWhiteSpace(FilterPhone) ? "_" : FilterPhone,
                filterEmail = string.IsNullOrWhiteSpace(FilterEmail) ? "_" : FilterEmail
            });
    }
}
