using UnityEngine;

public class JITutorialScript : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            tutorialPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
