using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using Zenject;

public class MouseInteractionsPresenter : MonoBehaviour
{

    #region Fields

    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _groundTransform;

    [Inject] private SelectableValue _selectedObject;
    [Inject] private AttackableValue _attackableObject;
    [Inject] private OutlinableValue _outlinedObject;
    [Inject] private Vector3Value _groundClicksRMB;

    #endregion

    #region Unity Events

    private void Start()
    {

        Observable
            .EveryUpdate()
            .Where(_ => !_eventSystem.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            .Subscribe(_ => RightMouseButtonReaction())
            .AddTo(this);

        Observable
            .EveryUpdate()
            .Where(_ => !_eventSystem.IsPointerOverGameObject() && Input.GetMouseButton(1))
            .Subscribe(_ => LeftMouseButtonUpReaction())
            .AddTo(this);

    }

    #endregion

    #region Methods

    private void LeftMouseButtonUpReaction()
    {

        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        var hit = Physics.Raycast(ray, out var enter);

        if (hit)
        {

            var pointable = GetHitObject<IAttackable>(enter);

            if (pointable != null)
            {

                _attackableObject.SetValue(pointable);
                _attackableObject.Notify();

                return;

            };

            var enterParent = enter.collider.transform;

            while (true)
            {
                if (!enterParent) break;

                if (enterParent.Equals(_groundTransform))
                {
                    
                    _groundClicksRMB.SetValue(ray.origin + ray.direction * enter.distance);
                    _groundClicksRMB.Notify();

                    return;

                };

                enterParent = enterParent.parent;

            };

        };

    }

    private void RightMouseButtonReaction()
    {

        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        var hits = Physics.RaycastAll(ray);

        if (hits.Length == 0) return;

        var selectable = GetHitObject<ISelectable>(hits);

        _selectedObject.SetValue(selectable);

        var outlinable = GetHitObject<IOutlinable>(hits);

        _outlinedObject.SetValue(outlinable);

    }

    private T GetHitObject<T>(RaycastHit[] hits)
    {
        return hits
                .Select(hit => hit.collider.GetComponentInParent<T>())
                .Where(x => x != null)
                .FirstOrDefault();
    }

    private T GetHitObject<T>(RaycastHit hit)
    {
        return hit.collider.GetComponentInParent<T>();
    }

    #endregion

}