namespace API.Common
{
    public class Paging
    {
        public Paging(int pageFind, int pageSize = 5)
        {
            PageSize = pageSize;
            PageFind = pageFind;
        }
        // default 1 page have 5 row 
        public int PageSize { get; set; } = 5;
        public int PageFind { get; set; } = 1;
    }
}
