using System.Runtime.CompilerServices;

public interface IAwaiter<TAwaited> : INotifyCompletion
{

    #region Properties

    bool IsCompleted { get; }

    #endregion

    #region Methods

    TAwaited GetResult();

    #endregion

}