namespace OPLOGMicroservice.Business.Core.Interfaces
{
    public interface IQuery<TResult> where TResult : class
    {

    }

    public interface IAsyncQuery<TResult> : IQuery<TResult> where TResult : class
    {

    }
}
