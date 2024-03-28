using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using System;

public class StartingTutorialPopup : BasePopup
{
    [SerializeField] private Text _tutorialBottomText;
    [SerializeField] private Image _boosterImg;
    [SerializeField] private Text _tutorialUpText;
    [SerializeField] private Button skipBtn;

    [SerializeField] private Sprite _freezSprite;
    [SerializeField] private Sprite _jumpUpSprite;
    [SerializeField] private Sprite _speedUpSprite;

    [Inject] private IPopupManager popupsManager;
    private void Start()
    {
        skipBtn.onClick.AddListener(() => { popupsManager.HideCurrentPopup(); Time.timeScale = 1; });
    }
    public override void AfterShow(string json)
    {
        BoosterType currentBoosterType = JsonUtility.FromJson<BoosterType>(json);
        if (currentBoosterType == BoosterType.None)
        {
            _tutorialBottomText.gameObject.SetActive(false);
            _tutorialUpText.text = "Tap and hold to aim and release to jump";
            _boosterImg.gameObject.SetActive(false);
        }
        else
        {
            if (currentBoosterType == BoosterType.JumpUp)
            {
                _tutorialBottomText.text = "Tap To Continue";
                _boosterImg.sprite = _jumpUpSprite;
                _tutorialUpText.text = "This is long jump power up, take it to make a long jump, active for 3 sec";
            }
            if (currentBoosterType == BoosterType.FreezeAll)
            {
                _tutorialBottomText.text = "Tap To Continue";
                _boosterImg.sprite = _freezSprite;
                _tutorialUpText.text = "This is freeze power up, take it to freeze your opponents, active for 3 sec";
            }
            if (currentBoosterType == BoosterType.SpeedUp)
            {
                _tutorialBottomText.text = "Tap To Continue";
                _boosterImg.sprite = _speedUpSprite;
                _tutorialUpText.text = "This is hight speed power up, take it to increase your speed and become invincible for 5 sec";
            }
        }
    }
}
