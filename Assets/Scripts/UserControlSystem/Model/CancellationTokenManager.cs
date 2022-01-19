using System.Threading;

public class CancellationTokenManager
{

    #region Fields

    private CancellationTokenSource _cancellationTokenSource;

    #endregion

    #region Methods

    public void CancelToken()
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            _cancellationTokenSource = null;
        };
    }

    public CancellationToken CreateToken()
    {
        CancelToken();

        _cancellationTokenSource = new CancellationTokenSource();

        return _cancellationTokenSource.Token;
    }

    #endregion

}