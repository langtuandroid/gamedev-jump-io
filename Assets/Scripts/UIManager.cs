using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Text levelNoText;
    [SerializeField] private Button boosterShopBtn;

    [Space]
    [Header("For Player")]
    [SerializeField] private Slider playerSlider;
    [SerializeField] private Transform playerStartPosition;
    [SerializeField] private Transform playerEndPosition;

    [Space]
    [Header("For 2nd Player")]
    [SerializeField] private Slider otherPlayer1Slider;
    [SerializeField] private Transform otherPlayer1StartPosition;

    [Space]
    [Header("For 3rd Player")]
    [SerializeField] private Slider otherPlayer2Slider;
    [SerializeField] private Transform otherPlayer2StartPosition;

    [SerializeField] private GameObject boosterShop;

    [Inject] private IPopupManager popusManager;
    [Inject] private GameManager gameManager;
    private int _levelIndex;

    private void Start()
    {
        gameManager.ON_LEVEL_COMPLETED_ACTION += OnLevelCompletedAction;
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
        levelNoText.text = "Level " + _levelIndex;

        boosterShopBtn.onClick.AddListener(OpenBoosterShop);
    }
    private void OpenBoosterShop()
    {
        Time.timeScale = 0;
        popusManager.ShowPopup(PopupType.BoosterShop);
    }
    private void OpenDiamodShop()
    {
        Time.timeScale = 0;
        popusManager.ShowPopup(PopupType.DiamondShop);
    }
    private void OnLevelCompletedAction(int place)
    {
        boosterShop.gameObject.SetActive(false);
        levelNoText.gameObject.SetActive(false);
        popusManager.ShowPopup(PopupType.LevelResultPopup, (place==1).ToString());
        _levelIndex++;
        if (_levelIndex <= 35 && PlayerPrefs.GetInt("Level No.") < _levelIndex)
        {
            Debug.Log("Level No." + _levelIndex);
            PlayerPrefs.SetInt("Level No.", _levelIndex);
        }
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
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
