namespace Ecommerce.Common
{
    public class ResponsePagination<T> : ResponseGeneric<T>
    {
        public int PageNumber { get; set; } 
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }   
        public bool HasPreviousPage =>PageNumber > 1; //indica si tiene pagina previa
        public bool HasNextPage =>PageNumber < TotalPages;   //indica si existe una pagina siguiente


    }
}
