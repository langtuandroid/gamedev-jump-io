using UnityEngine;

public interface IPopup
{
    void Show();
    void Hide();
  
}
public interface IAfterShow
{
    public void AfterShow();
}
public class BasePopup : MonoBehaviour, IPopup, IAfterShow
{
    [SerializeField]
    private PopupType _type = PopupType.None;

    public PopupType Type => _type;

    void IPopup.Show()
    {
        gameObject.SetActive(true);
        AfterShow();
    }
     public virtual void AfterShow()
    {

    }
    void IPopup.Hide()
    {
        gameObject.SetActive(false);
    }
}