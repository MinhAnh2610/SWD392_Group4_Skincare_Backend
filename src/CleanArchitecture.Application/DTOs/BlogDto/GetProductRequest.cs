namespace CleanArchitecture.Application.DTOs.BlogDto
{
  public class GetProductRequest(
    string? title,
    string? content,
    string? sortColumn,
    string? sortOrder,
    string[]? tags,
    string? staffUsername,
    int pageIndex,
    int pageSize)
  {
    public string? Title { get; set; } = title;
    public string? Content { get; set; } = content;
    public string? SortColumn { get; set; } = sortColumn;
    public string? SortOrder { get; set; } = sortOrder;
    public string[]? Tags { get; set; } = tags;
    public string? StaffUsername { get; set; } = staffUsername;
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
  }
}