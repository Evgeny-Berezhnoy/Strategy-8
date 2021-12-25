using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class MouseInteractionsPresenter : MonoBehaviour
{

    #region Fields

    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _groundTransform;

    [Inject] private SelectableValue _selectedObject;
    [Inject] private PointableValue _pointedObject;
    [Inject] private OutlinableValue _outlinedObject;
    [Inject] private Vector3Value _groundClicksRMB;
    
    #endregion

    #region Unity Events

    private void Update()
    {

        var inputLeftMouseButtonUp  = Input.GetMouseButtonUp(0);
        var inputRightMouseButton   = Input.GetMouseButton(1);

        if (!(inputLeftMouseButtonUp || inputRightMouseButton)) return;

        if (_eventSystem.IsPointerOverGameObject()) return;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (inputRightMouseButton)
        {

            var hit = Physics.Raycast(ray, out var enter);

            if(hit)
            {

                var pointable = GetHitObject<IPointable>(enter);

                if (pointable != null)
                {

                    _pointedObject.SetValue(pointable);

                    return;

                };

                var enterParent = enter.collider.transform;

                while (true)
                {

                    if (!enterParent) break;

                    if(enterParent.Equals(_groundTransform))
                    {

                        _groundClicksRMB.SetValue(ray.origin + ray.direction * enter.distance);

                        return;

                    };

                    enterParent = enterParent.parent;

                };

            };

        }
        else if (inputLeftMouseButtonUp)
        {

            var hits = Physics.RaycastAll(ray);

            if (hits.Length == 0) return;

            var selectable = GetHitObject<ISelectable>(hits);

            _selectedObject.SetValue(selectable);
            
            var outlinable = GetHitObject<IOutlinable>(hits);

            _outlinedObject.SetValue(outlinable);

        };

    }

    #endregion

    #region Methods

    private T GetHitObject<T>(RaycastHit[] hits)
    {

        return
            hits
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