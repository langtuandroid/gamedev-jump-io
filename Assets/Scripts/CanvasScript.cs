using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextMeshProUGUI levelNoText;

    [Space]
    [Header("For Player")]
    public Slider playerSlider;
    public Transform playerStartPosition;
    public Transform playerEndPosition;

    [Space]
    [Header("For 2nd Player")]
    public Slider otherPlayer1Slider;
    public Transform otherPlayer1StartPosition;

    [Space]
    [Header("For 3rd Player")]
    public Slider otherPlayer2Slider;
    public Transform otherPlayer2StartPosition;


    int levelNo;
    

    private void Start()
    {
        levelNo = SceneManager.GetActiveScene().buildIndex;
        levelNoText.text = "Level " + levelNo;
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
        SceneManager.LoadScene(levelNo);
    }

    public void Next()
    {
        levelNo++;
        if(levelNo > 35)
        {
            levelNo = 1;
        }
        PlayerPrefs.SetInt("Level No.", levelNo);
        SceneManager.LoadScene(levelNo);
    }
}
