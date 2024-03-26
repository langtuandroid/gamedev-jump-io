using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BootstrapperInstaller : MonoInstaller
{
    [SerializeField] private PopupsManager popupManager;
    public override void InstallBindings()
    {
        Container.Bind<IPopupManager>().FromInstance(popupManager).AsSingle();
    }
}
