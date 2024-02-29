using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvasScript : MonoBehaviour
{
    int levelNo;
    private void Awake()
    {
        levelNo = PlayerPrefs.GetInt("Level No.", 1);

        SceneManager.LoadScene(levelNo);
    }
}
