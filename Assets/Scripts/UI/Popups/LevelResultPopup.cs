using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using Integration;
public class LevelResultPopup : BasePopup
{
    [SerializeField] private GameObject restartLevelBtn;
    [SerializeField] private GameObject nextLevelBtn;
    [Inject] private AdMobController adMobController;

    bool isWin = false;
    private void Start()
    {
        restartLevelBtn.GetComponent<Button>().onClick.AddListener(() => Reload()) ;
        nextLevelBtn.GetComponent<Button>().onClick.AddListener(() => Next());

        adMobController.InterstitialAdController.OnAdClosed += () =>
        {
            if(isWin==false)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex >= 35 ? 0 : SceneManager.GetActiveScene().buildIndex + 1);
        };
    }
    public override void AfterShow(string json)
    {
        bool isFirstPlace =bool.Parse(json);
        if (isFirstPlace)
            nextLevelBtn.SetActive(true);
        else
            restartLevelBtn.SetActive(true);
    }
    private void Reload()
    {
        isWin = false;
        //if(isShowInterstital vs subscription)
        adMobController.ShowInterstitialAd();
    }

    private void Next()
    {
        isWin = true;

        adMobController.ShowInterstitialAd();
    }
}
