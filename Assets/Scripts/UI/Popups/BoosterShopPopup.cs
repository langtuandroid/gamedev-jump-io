using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Integration;
using UnityEngine.Events;

public class BoosterShopPopup : BasePopup
{
    [SerializeField] private Button _backBtn;
    [SerializeField] private Button buyFreezeBoosterBtn;
    [SerializeField] private Button buySpeedUpBoosterBtn;
    [SerializeField] private Button buyJumpUpBoosterBtn;
    [SerializeField] private Button diamontBtn;
    [SerializeField] private Text diamondValueText;

    [Inject] private GameManager _gameManager;
    [Inject] private IPopupManager popupsManager;
    [Inject] private PlayerBehaviour _playerBehaviour;
    [Inject] private Wallet _wallet;
    private void Awake()
    {
        _backBtn.onClick.AddListener(BackBtnOnClick);
        buyFreezeBoosterBtn.onClick.AddListener(buyFreezeBoosterBtnOnClick);
        buySpeedUpBoosterBtn.onClick.AddListener(buySpeedUpBoosterBtnOnClick);
        buyJumpUpBoosterBtn.onClick.AddListener(buyJumpUpBoosterBtnOnClick);
        diamontBtn.onClick.AddListener(() => { popupsManager.ShowPopup(PopupType.DiamondShop); });
        _wallet.GemsChange.AddListener((int a) => { diamondValueText.text = a.ToString(); });
    }
    private void OnEnable()
    {
        CheckAvailbleBooster();
    }
    private void Start()
    {
        diamondValueText.text = _wallet.GetGems().ToString();
    }
    private void BackBtnOnClick()
    {
        popupsManager.HideCurrentPopup(); Time.timeScale = 1;
        _gameManager.EnableJump();
    }
    
    private void CheckAvailbleBooster()
    {
        if(_gameManager.otherPlayerFreeze ||_gameManager.fastSpeedOn)
        {
            buyFreezeBoosterBtn.interactable = false;
            buySpeedUpBoosterBtn.interactable = false;
            buyJumpUpBoosterBtn.interactable = false;
        }
        else
        {
            if (_gameManager.otherPlayerFreeze==false & _gameManager.fastSpeedOn==false)
            {
                buyFreezeBoosterBtn.interactable = true;
                buySpeedUpBoosterBtn.interactable = true;
                buyJumpUpBoosterBtn.interactable = true;
            }
        }
    }
    private void buyFreezeBoosterBtnOnClick()
    {
        if (_wallet.GetGems() >= 100)
        {
            _wallet.SetGems(-100);
            _playerBehaviour.FreezeenemyBooster();
            CheckAvailbleBooster();
        }
    }
    private void buySpeedUpBoosterBtnOnClick()
    {
        if (_wallet.GetGems() >= 100)
        {
            _wallet.SetGems(-100);
            _playerBehaviour.FastSpeedUpBooster();
            CheckAvailbleBooster();
        }
    }
    private void buyJumpUpBoosterBtnOnClick()
    {
        if (_wallet.GetGems() >= 100)
        {
            _wallet.SetGems(-100);
            _gameManager.trajectoryOn = false;
            _playerBehaviour.LongUpBooster();
            CheckAvailbleBooster();
        }
    }
}
