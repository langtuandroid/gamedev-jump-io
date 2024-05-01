using Integration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BootstrapperInstaller : MonoInstaller
{
    [SerializeField] private PopupsManager popupManager;
    [SerializeField] private GameManager gameManager;

    [Inject] private AdMobController _adMobController;
    public override void InstallBindings()
    {
        Container.Bind<IPopupManager>().FromInstance(popupManager).AsSingle();
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
        Container.Bind<PlayerBehaviour>().FromInstance(FindAnyObjectByType<PlayerBehaviour>()).AsSingle();
    }
    private void Start()
    {
        _adMobController.ShowBanner(true);
    }
}
