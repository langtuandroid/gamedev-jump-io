using UnityEngine;
using Zenject;
public class ProjectInstallerMoj : MonoInstaller
{
    [SerializeField] private GameObject _audioManagerPrefab;
    [SerializeField] private Wallet _walletObj;
    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromComponentInNewPrefab(_audioManagerPrefab).AsSingle().NonLazy();

        Container.Bind<Wallet>().FromInstance(_walletObj).AsSingle();
    }
}
