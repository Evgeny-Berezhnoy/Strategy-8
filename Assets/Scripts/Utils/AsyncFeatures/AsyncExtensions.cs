using System.Threading;
using System.Threading.Tasks;

public static class AsyncExtensions
{

    #region Structs

    public struct Void { }

    #endregion

    #region Methods

    public static async Task<TResult> WithCancellation<TResult>(this Task<TResult> originalTask, CancellationToken cancellationToken)
    {
        var cancelTask = new TaskCompletionSource<Void>();

        using (cancellationToken.Register(t => ((TaskCompletionSource<Void>) t).TrySetResult(new Void()), cancelTask))
        {
            var firstCompletedTask = await Task.WhenAny(originalTask, cancelTask.Task);

            if(firstCompletedTask == cancelTask.Task)
            {
                cancellationToken.ThrowIfCancellationRequested();
            };
        };

        return await originalTask;

    }

    public static async Task<TResult> WithCancellation<TResult>(this IAwaitable<TResult> originalTask, CancellationToken cancellationToken)
    {
        return await WithCancellation(originalTask.AsTask(), cancellationToken);
    }

    public static Task<TResult> AsTask<TResult>(this IAwaitable<TResult> awaitable)
    {
        return Task.Run(async() => await awaitable);
    }

    #endregion

}