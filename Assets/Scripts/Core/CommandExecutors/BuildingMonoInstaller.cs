using UnityEngine;
using Zenject;

[RequireComponent(typeof(FactionMemberParrallelInfoUpdater))]
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

        Container
            .Bind<ITickable>()
            .FromInstance(GetComponent<ITickable>());
    }

    #endregion

}