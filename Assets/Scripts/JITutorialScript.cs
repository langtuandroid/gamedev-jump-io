using UnityEngine;

public class JITutorialScript : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            PopupsManager.Instance.ShowPopup(PopupType.StartingTutorialPopup);
        }
    }
}
