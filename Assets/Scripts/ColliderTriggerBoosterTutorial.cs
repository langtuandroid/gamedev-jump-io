using UnityEngine;
using Zenject;
public enum BoosterType
{
    None, 
    FreezeAll,
    JumpUp,
    SpeedUp
}
public class ColliderTriggerBoosterTutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [Inject] private IPopupManager _popupManager;
    [Inject] private GameManager gameManager;
    private BoosterType currentBoosterType;
    private void Start()
    {
        if (gameManager.IsUseBoosterFreezeAll)
            currentBoosterType = BoosterType.FreezeAll;
        else
        if (gameManager.IsUseBoosterJumpUp)
            currentBoosterType = BoosterType.JumpUp;
        else
            if(gameManager.IsUseBoosterSpeedUp)
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
