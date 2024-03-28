using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButtonController : MonoBehaviour
{
    [SerializeField] private Button _thisButton;
    [SerializeField] private Text _levelText;
    [SerializeField] private Image _lockImage;
    private int _levelIndex;
    
    public void Initialize(int levelIndex)
    {
        _levelIndex = levelIndex;
        var isUnlocked = _levelIndex <= PlayerPrefs.GetInt("Level No.", 1);
        _lockImage.enabled = !isUnlocked;
        _thisButton.interactable = isUnlocked;
        if (isUnlocked)
            _levelText.text = _levelIndex.ToString();
        else
            _levelText.text = "";
        _thisButton.onClick.AddListener(SelectLevel);
    }

    private void SelectLevel()
    {
        Debug.Log("levelindex "+ _levelIndex);
        SceneManager.LoadScene(_levelIndex);
    }
}