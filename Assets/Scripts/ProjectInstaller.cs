using UnityEngine;
using Zenject;
public class ProjectInstaller : MonoInstaller
{
   // [SerializeField] private JIGameManager gameManager;
    [SerializeField] private GameObject _audioManagerPrefab;
    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromComponentInNewPrefab(_audioManagerPrefab).AsSingle().NonLazy();

       // Container.Bind<JIGameManager>().FromInstance(gameManager).AsSingle();
    }
}
