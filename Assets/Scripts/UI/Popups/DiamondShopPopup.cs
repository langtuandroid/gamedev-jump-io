using Integration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DiamondShopPopup : BasePopup
{
    [SerializeField] private Button _backBtn;
    [SerializeField] private Button buyDiamond100Btn;
    [SerializeField] private Button buyDiamond300Btn;
    [SerializeField] private Button buyDiamond1000Btn;
    [SerializeField] private Button buyDiamond3000Btn;

    [Inject] private IPopupManager popupsManager;
    [Inject] private IAPService _IAPService;
    private void Awake()
    {
        _backBtn.onClick.AddListener(BackBtnOnClick);
        buyDiamond100Btn.onClick.AddListener(_IAPService.BuyPack1);
        buyDiamond300Btn.onClick.AddListener(_IAPService.BuyPack2);
        buyDiamond1000Btn.onClick.AddListener(_IAPService.BuyPack3);
        buyDiamond3000Btn.onClick.AddListener(_IAPService.BuyPack4);
    }
    private void BackBtnOnClick()
    {
        popupsManager.HideCurrentPopup(); Time.timeScale = 1;
    }

}
