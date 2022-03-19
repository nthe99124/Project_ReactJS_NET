namespace API.Common
{
    public class Paging
    {
        // default 1 page have 5 row 
        public int pageSize = 5;
        public int pageFind = 1;
        public string pagingOrderBy;
        /// <summary>
        /// "asc" or "desc"
        /// </summary>
        public string typeSort = "asc";
    }
}
