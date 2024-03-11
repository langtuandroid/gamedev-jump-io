using UnityEngine;
using UnityEngine.SceneManagement;

public class JIMainCanvasScript : MonoBehaviour
{
    private int _levelIndex;
    
    private void Awake()
    {
        _levelIndex = PlayerPrefs.GetInt("Level No.", 1);

        SceneManager.LoadScene(_levelIndex);
    }
}
