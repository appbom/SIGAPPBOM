namespace SIGAPPBOM.Logistica.Web.Pagination
{
    public interface IPagedList
    {
        int TotalItems { get; set; }
        int ItemsPerPage { get; set; }
        int CurrentPage { get; set; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        int TotalPages { get; }
    }
}