using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JICanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI levelNoText;

    [Space]
    [Header("For Player")]
    [SerializeField] private Slider playerSlider;
    [SerializeField] private Transform playerStartPosition;
    [SerializeField] private Transform playerEndPosition;
    [SerializeField] private GameObject tryAgainButton;
    [SerializeField] private GameObject continueButton;

    [Space]
    [Header("For 2nd Player")]
    [SerializeField] private Slider otherPlayer1Slider;
    [SerializeField] private Transform otherPlayer1StartPosition;

    [Space]
    [Header("For 3rd Player")]
    [SerializeField] private Slider otherPlayer2Slider;
    [SerializeField] private Transform otherPlayer2StartPosition;

    private int _levelIndex;

    private void Start()
    {
        JIGameManager.Instance.ON_LEVEL_COMPLETED_ACTION += OnLevelCompletedAction;
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
        levelNoText.text = "Level " + _levelIndex;
    }

    private void OnLevelCompletedAction(int place)
    {
        tryAgainButton.SetActive(place > 1);
        continueButton.SetActive(place == 1);
        _levelIndex++;
        if(_levelIndex <= 35 && PlayerPrefs.GetInt("Level No.") < _levelIndex) PlayerPrefs.SetInt("Level No.", _levelIndex);
    }

    private void Update()
    {
        playerSlider.value = playerStartPosition.position.z / playerEndPosition.position.z;

        if(otherPlayer2Slider)
        {
            otherPlayer1Slider.value = otherPlayer1StartPosition.position.z / playerEndPosition.position.z;
        }

        if(otherPlayer2Slider)
        {
            otherPlayer2Slider.value = otherPlayer2StartPosition.position.z / playerEndPosition.position.z;
        }

    }
    
    public void TutorialPanelOff()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex >= 35 ? 0 : SceneManager.GetActiveScene().buildIndex + 1);
    }
}
