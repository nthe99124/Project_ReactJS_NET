namespace API.Common
{
    public class Paging
    {
        // default 1 page have 5 row 
        public int PageSize = 5;
        public int PageFind = 1;
        public string PagingOrderBy;
        /// <summary>
        /// "asc" or "desc"
        /// </summary>
        public string TypeSort = "asc";
    }
}
