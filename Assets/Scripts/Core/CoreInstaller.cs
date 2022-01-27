using Zenject;

public class CoreInstaller : MonoInstaller
{

    #region Base methods

    public override void InstallBindings()
    {
        
        Container
            .BindInterfacesAndSelfTo<TimeModel>()
            .AsSingle();

    }

    #endregion

}