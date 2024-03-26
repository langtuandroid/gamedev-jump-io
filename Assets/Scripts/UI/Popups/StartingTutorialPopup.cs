using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using System;

public class StartingTutorialPopup : BasePopup
{
    [SerializeField] private TMP_Text _tutorialBottomText;
    [SerializeField] private Image _boosterImg;
    [SerializeField] private TMP_Text _tutorialUpText;
    [SerializeField] private Button skipBtn;

    [SerializeField] private Sprite _freezSprite;

    [SerializeField] private Sprite _jumpUpSprite;

    [Inject] private IPopupManager _popupsManager;
    private void Start()
    {
        skipBtn.onClick.AddListener(()=> { _popupsManager.HideCurrentPopup(); Time.timeScale = 1; });
    }
    public override void AfterShow(string json)
    {
        BoosterType currentBoosterType = JsonUtility.FromJson<BoosterType>(json);
        if(currentBoosterType == BoosterType.None)
        {
            _tutorialBottomText.text = "Tap and hold to aim and release to jump.";
            _boosterImg.gameObject.SetActive(false);
            _tutorialUpText.gameObject.SetActive(false);
        }
        else
        {
            if(currentBoosterType == BoosterType.JumpUp)
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
        }
    }
}
