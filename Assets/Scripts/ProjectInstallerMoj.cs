using UnityEngine;
using Zenject;
public class ProjectInstallerMoj : MonoInstaller
{
   // [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject _audioManagerPrefab;
    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromComponentInNewPrefab(_audioManagerPrefab).AsSingle().NonLazy();

       // Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
    }
}
