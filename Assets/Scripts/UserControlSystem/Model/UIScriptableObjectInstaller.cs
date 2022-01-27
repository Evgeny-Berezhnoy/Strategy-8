using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(UIScriptableObjectInstaller), menuName = "Strategy Game/" + nameof(UIScriptableObjectInstaller), order = 2)]
public class UIScriptableObjectInstaller : ScriptableObjectInstaller
{

    #region Fields

    [SerializeField] private SelectableValue _selectableValue;
    [SerializeField] private Vector3Value _vector3Value;
    [SerializeField] private AssetsContext _context;
    [SerializeField] private OutlinableValue _outlinableValue;
    [SerializeField] private PointableValue _pointableValue;
    [SerializeField] private Sprite _zerglingSprite;

    #endregion

    #region Base Methods

    public override void InstallBindings()
    {

        Container
            .Bind<SelectableValue>()
            .FromInstance(_selectableValue);

        Container
            .Bind<IObservable<ISelectable>>()
            .FromInstance(_selectableValue);

        Container
            .Bind<Vector3Value>()
            .FromInstance(_vector3Value);

        Container
            .Bind<IAwaitable<Vector3>>()
            .FromInstance(_vector3Value);
        
        Container
            .Bind<AssetsContext>()
            .FromInstance(_context);

        Container
            .Bind<OutlinableValue>()
            .FromInstance(_outlinableValue);

        Container
            .Bind<PointableValue>()
            .FromInstance(_pointableValue);
        
        Container
            .Bind<IAwaitable<IPointable>>()
            .FromInstance(_pointableValue);

        Container
            .Bind<Sprite>()
            .WithId("Zergling")
            .FromInstance(_zerglingSprite);

    }

    #endregion

}