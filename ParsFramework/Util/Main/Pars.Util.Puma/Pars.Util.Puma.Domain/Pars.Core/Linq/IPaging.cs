namespace Pars.Core.Linq
{
    public interface IPaging
    {
        int PageNumber { get; set; }
     
        int PageSize { get; set; }
    }
}
