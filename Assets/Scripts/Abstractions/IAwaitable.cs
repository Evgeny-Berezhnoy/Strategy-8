public interface IAwaitable<T>
{

    #region Properties

    IAwaiter<T> GetAwaiter();

    #endregion

}