using UnityEngine;
using Zenject;
public enum BoosterType
{
    None, 
    FreezeAll,
    JumpUp,
    SpeedUp
}
public class JITutorialScript : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [Inject] private IPopupManager _popupManager;
    private BoosterType currentBoosterType;
    private void Start()
    {
        if (JIGameManager.Instance.IsUseBoosterFreezeAll)
            currentBoosterType = BoosterType.FreezeAll;
        else
        if (JIGameManager.Instance.IsUseBoosterJumpUp)
            currentBoosterType = BoosterType.JumpUp;
        else
            if(JIGameManager.Instance.IsUseBoosterSpeedUp)
            currentBoosterType = BoosterType.SpeedUp;
        else
            currentBoosterType = BoosterType.None;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            _popupManager.ShowPopup(PopupType.StartingTutorialPopup, JsonUtility.ToJson(currentBoosterType));
        }
    }
}
