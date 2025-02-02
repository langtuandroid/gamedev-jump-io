using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGeneratorController : MonoBehaviour
{
    [SerializeField] private LevelSelectButtonController _buttonPrefab;
    [SerializeField] private RectTransform _levelsPanel;
    
    void Start()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var levelButton = Instantiate(_buttonPrefab, _levelsPanel);
            levelButton.Initialize(i);
        }
    }
}
