using UnityEngine;
using UnityEngine.UI;
namespace UiControllers
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _backToMainMenuButton;
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _levelsPanel;
        [SerializeField] private GameObject _settingPanel;
        [SerializeField] private Image _soundIconImage;
        [SerializeField] private GameObject _gameNameText;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Sprite _mainSprite;
        [SerializeField] private Sprite _secondSprite;

        private void Start()
        {
            _backToMainMenuButton.SetActive(false);
            _levelsPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
            if (PlayerPrefs.GetInt("Audio", 0) != 0)
                _soundIconImage.color = new Color(0.5f, 0.5f, 0.5f);
            else
                _soundIconImage.color = new Color(1, 1, 1);
            JIBackgroundMusic.Instance.gameObject.SetActive(PlayerPrefs.GetInt("Audio", 0) == 0);
        }

        public void SettingsButton()
        {
            _backgroundImage.sprite = _secondSprite;
            _backToMainMenuButton.SetActive(true);
            _mainMenuPanel.SetActive(false);
            _settingPanel.SetActive(true);
            _gameNameText.SetActive(false);
        }

        public void AudioButton()
        {
            PlayerPrefs.SetInt("Audio", PlayerPrefs.GetInt("Audio", 0) == 0 ? 1 : 0);

            if (PlayerPrefs.GetInt("Audio", 0) != 0)
                _soundIconImage.color = new Color(0.5f, 0.5f, 0.5f);
            else
                _soundIconImage.color = new Color(1, 1, 1);
            JIBackgroundMusic.Instance.gameObject.SetActive(PlayerPrefs.GetInt("Audio", 0) == 0);
        }
        public void PlayButton()
        {
            _backgroundImage.sprite = _secondSprite;
            _backToMainMenuButton.SetActive(true);
            _levelsPanel.SetActive(true);
            _mainMenuPanel.SetActive(false);
            _gameNameText.SetActive(false);
        }

        public void BackToMainMenuButton()
        {
            _backgroundImage.sprite = _mainSprite;
            _settingPanel.SetActive(false);
            _backToMainMenuButton.SetActive(false);
            _levelsPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
            _gameNameText.SetActive(true);
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
}
