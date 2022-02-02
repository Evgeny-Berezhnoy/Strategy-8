using Zenject;

public class GrenadierMonoInstaller : MonoInstaller
{
    #region Base methods

    public override void InstallBindings()
    {
        Container
            .Bind<float>()
            .WithId("Death duration")
            .FromInstance(6f);

        Container
            .Bind<CancellationTokenManager>()
            .AsSingle();
    }

    #endregion
}