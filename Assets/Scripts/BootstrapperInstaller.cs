using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BootstrapperInstaller : MonoInstaller
{
    [SerializeField] private PopupsManager popupManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TrajectoryManager trajectoryManager;
    public override void InstallBindings()
    {
        Container.Bind<IPopupManager>().FromInstance(popupManager).AsSingle();
        Container.Bind<TrajectoryManager>().FromInstance(trajectoryManager).AsSingle();
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
    }
}
