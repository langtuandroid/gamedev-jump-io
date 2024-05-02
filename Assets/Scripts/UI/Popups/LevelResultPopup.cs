using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using Integration;
using UnityEngine.Events;

public class LevelResultPopup : BasePopup
{
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private GameObject succesPanel;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button nextbtn;

    [Inject] private GameManager _gameManager;
    bool isWin = false;
    private void Start()
    {
        restartBtn.onClick.AddListener(() => Reload()) ;
        nextbtn.onClick.AddListener(() => Next());
    }
    public override void AfterShow(string json)
    {
        bool isFirstPlace =bool.Parse(json);
        if (isFirstPlace)
        {
            succesPanel.SetActive(true);
            nextbtn.interactable = false;
        }
        else
        {
            restartPanel.SetActive(true);
            restartBtn.interactable = false;
        }
        StartCoroutine(WaitAndAction(1,()=> { 
            _gameManager.AfterLevelLogic();
            restartBtn.interactable = true;
            nextbtn.interactable = true; 
        }));
    }
    IEnumerator WaitAndAction(float duration, UnityAction action )
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex >= 35 ? 0 : SceneManager.GetActiveScene().buildIndex + 1);
    }
}
