using Zenject;

public class UnitMonoInstaller : MonoInstaller
{

    #region Base methods

    public override void InstallBindings()
    {

        Container
            .Bind<CancellationTokenManager>()
            .AsSingle();

        Container
            .Bind<IHealthHolder>()
            .FromComponentInChildren();

        Container
            .Bind<float>()
            .WithId("AttackDistance")
            .FromInstance(5f);

        Container
            .Bind<int>()
            .WithId("AttackPeriod")
            .FromInstance(1400);

        Container
            .Bind<float>()
            .WithId("ReachingDistance")
            .FromInstance(0.3f);

        Container
            .Bind<float>()
            .WithId("FluctuationDistance")
            .FromInstance(3f);

    }

    #endregion

}