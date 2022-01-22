using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MenuPresenter : MonoBehaviour
{

    #region Fields

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _exitButton;

    #endregion

    #region Unity events

    private void Start()
    {
        _backButton.OnClickAsObservable().Subscribe(_ => gameObject.SetActive(false));
        _exitButton.OnClickAsObservable().Subscribe(_ =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }); 
    }

#endregion

}