using UnityEngine;

public interface IPopup
{
    void Show(string json);
    void Hide();
    public void AfterShow(string json);
}
public class BasePopup : MonoBehaviour, IPopup
{
    [SerializeField]
    private PopupType _type = PopupType.None;

    public PopupType Type => _type;

    void IPopup.Show(string json )
    {
        gameObject.SetActive(true);
        AfterShow(json);
    }
    public virtual void AfterShow(string json)
    {

    }
    void IPopup.Hide()
    {
        gameObject.SetActive(false);
    }
}