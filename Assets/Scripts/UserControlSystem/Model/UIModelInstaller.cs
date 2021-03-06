using Zenject;

public class UIModelInstaller : MonoInstaller
{

    #region Methods

    public override void InstallBindings()
    {

        Container
            .Bind<CommandCreatorBase<IProduceUnitCommand>>()
            .To<ProduceUnitCommandCreator>()
            .AsTransient();

        Container
            .Bind<CommandCreatorBase<IAttackCommand>>()
            .To<AttackCommandCreator>()
            .AsTransient();

        Container
            .Bind<CommandCreatorBase<IMoveCommand>>()
            .To<MoveCommandCreator>()
            .AsTransient();

        Container
            .Bind<CommandCreatorBase<IPatrolCommand>>()
            .To<PatrolCommandCreator>()
            .AsTransient();

        Container
            .Bind<CommandCreatorBase<IStopCommand>>()
            .To<StopCommandCreator>()
            .AsTransient();

        Container
            .Bind<CommandCreatorBase<IRendezvousPointCommand>>()
            .To<RendezvousPointCommandCreator>()
            .AsTransient();

        Container
            .Bind<CommandButtonsModel>()
            .AsTransient();

        Container
            .Bind<float>()
            .WithId("Zergling")
            .FromInstance(0.5f);
        
        Container
            .Bind<string>()
            .WithId("Zergling")
            .FromInstance("Zergling");

        Container
            .Bind<BottomCenterModel>()
            .AsSingle();

        Container
            .Bind<CancellationTokenManager>()
            .AsCached();

    }

    #endregion

}