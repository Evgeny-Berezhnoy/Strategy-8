using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInteractionsPresenter : MonoBehaviour
{

    #region Fields

    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableValue _selectedObject;

    [SerializeField] private EventSystem _eventSystem;

    #endregion

    #region Unity Events

    private void Update()
    {

        if (!Input.GetMouseButtonUp(0)) return;

        if (_eventSystem.IsPointerOverGameObject()) return;

        var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
        
        if (hits.Length == 0) return;

        var selectable = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .Where(x => x != null)
                .FirstOrDefault();

        {
        
            if (selectable is IOutlinable outlinableObject)
            {

                outlinableObject.OutlineDraw.enabled = true;

            };
        
        };

        {

            if (_selectedObject.CurrentValue is IOutlinable outlinableObject)
            {

                outlinableObject.OutlineDraw.enabled = false;

            };

        };

        _selectedObject.SetValue(selectable);
    
    }

    #endregion

}