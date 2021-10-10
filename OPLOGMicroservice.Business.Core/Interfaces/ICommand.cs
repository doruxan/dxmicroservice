namespace OPLOGMicroservice.Business.Core.Interfaces
{
    public interface ICommand<T>
    {
        bool Result { get; set; }
        T ReturnValue { get; set; }
    }

    public interface IAsyncCommand<T> : ICommand<T> { }
}
