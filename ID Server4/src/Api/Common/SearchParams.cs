using System.ComponentModel.DataAnnotations;

namespace Api.Common
{
    public class SearchParams
    {
    }

    public class PagingParams
    {
        //[Required]
        public int PageNo { get; set; }

        public int PageSize { get; set; }
    }
}
