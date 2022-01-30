using Zenject;

public class BuildingMonoInstaller : MonoInstaller
{

    #region Base methods

    public override void InstallBindings()
    {
        
        Container
            .Bind<CancellationTokenManager>()
            .AsSingle();

        Container
            .Bind<CommandCreatorBase<IMoveCommand>>()
            .To<MoveCommandCreator>()
            .AsTransient();

    }

    #endregion

}