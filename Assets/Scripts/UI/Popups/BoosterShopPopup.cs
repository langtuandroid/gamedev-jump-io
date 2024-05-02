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

    [Inject] private GameManager _gameManager;
    [Inject] private IPopupManager popupsManager;
    [Inject] private PlayerBehaviour _playerBehaviour;
    private void Awake()
    {
        _backBtn.onClick.AddListener(BackBtnOnClick);
        buyFreezeBoosterBtn.onClick.AddListener(buyFreezeBoosterBtnOnClick);
        buySpeedUpBoosterBtn.onClick.AddListener(buySpeedUpBoosterBtnOnClick);
        buyJumpUpBoosterBtn.onClick.AddListener(buyJumpUpBoosterBtnOnClick);
    }
    private void OnEnable()
    {
        CheckAvailbleBooster();
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
        _playerBehaviour.FreezeenemyBooster();
        CheckAvailbleBooster();
    }
    private void buySpeedUpBoosterBtnOnClick()
    {
        _playerBehaviour.FastSpeedUpBooster();
        CheckAvailbleBooster();
    }
    private void buyJumpUpBoosterBtnOnClick()
    {
        _gameManager.trajectoryOn = false;
        _playerBehaviour.LongUpBooster();
        CheckAvailbleBooster();
    }
}
