using Zenject;

public class FactionMonoInstaller : MonoInstaller
{
    #region Base methods

    public override void InstallBindings()
    {

        Container
            .Bind<IFactionCounter>()
            .To<FactionCounterModel>()
            .AsCached();

    }

    #endregion
}