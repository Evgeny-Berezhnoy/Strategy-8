using System.Collections.Generic;

public interface ICommandQueueManager
{

    #region Properties

    Queue<ExecutorWrapper> CommandQueue { get; }

    #endregion

    #region Methods

    void Enqueue(ICommandExecutor commandExecutor, object command);

    void Clear();

    #endregion

}