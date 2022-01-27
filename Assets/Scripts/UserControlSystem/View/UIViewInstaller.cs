using Zenject;

public class UIViewInstaller : MonoInstaller
{

    #region Base methods

    public override void InstallBindings()
    {
        Container
            .Bind<BottomCenterView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    #endregion

}