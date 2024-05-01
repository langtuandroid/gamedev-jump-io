using System;
using UnityEngine;
using Zenject;

public enum PopupType
{
    None = 0,
    StartLevelPopup=1,
    StartingTutorialPopup=2,
    LevelResultPopup=3,
    DiamondShop=4
}
public interface IPopupManager
{
    public BasePopup ShowPopup(PopupType t, string json = null);
    public void HideCurrentPopup();
}
public class PopupsManager : MonoBehaviour, IPopupManager
{
    public class Factory : PlaceholderFactory<PopupsManager>{}
    [SerializeField]
    private BasePopup[] _popups = null;

    private BasePopup _activePopup;
        
    public BasePopup ShowPopup(PopupType t, string json = null)
    {
        HideCurrentPopup();

        BasePopup p = Array.Find(_popups, x => x.Type == t);
        if (p != null)
        {
            ((IPopup)p).Show(json);
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