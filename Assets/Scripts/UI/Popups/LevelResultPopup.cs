using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelResultPopup : BasePopup
{
    [SerializeField] private GameObject restartLevelBtn;
    [SerializeField] private GameObject nextLevelBtn;
    private void Start()
    {
        restartLevelBtn.GetComponent<Button>().onClick.AddListener(() => Reload()) ;
        nextLevelBtn.GetComponent<Button>().onClick.AddListener(() => Next());
    }
    public override void AfterShow(string json)
    {
        bool isFirstPlace;
        bool.TryParse(json, out isFirstPlace);
        if (isFirstPlace)
            nextLevelBtn.SetActive(true);
        else
            restartLevelBtn.SetActive(true);
    }
    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex >= 35 ? 0 : SceneManager.GetActiveScene().buildIndex + 1);
    }
}
