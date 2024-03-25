using System;
using UnityEngine;

public enum PopupType
{
    None = 0,
    TimerPopup=1
}

public class PopupsManager : MonoBehaviour
{
    public static PopupsManager Instance { get; private set; }

    [SerializeField]
    private BasePopup[] _popups = null;

    private BasePopup _activePopup;

    [ContextMenu("FindPopups")]
    private void FindPopups()
    {
        _popups = GetComponentsInChildren<BasePopup>(true);
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }
    public BasePopup ShowPopup(PopupType t)
    {
        HideCurrentPopup();

        BasePopup p = Array.Find(_popups, x => x.Type == t);
        if (p != null)
        {
            ((IPopup)p).Show();
            _activePopup = p;

            return p;
        }
        else
        {
            Debug.LogError($"[PopupsManager.ShowPopup] Can't find popup with type {t}");
            return null;
        }
    }

    public void HideCurrentPopup()
    {
        if (_activePopup != null)
        {
            ((IPopup)_activePopup).Hide();
            _activePopup = null;
        }
    }
}