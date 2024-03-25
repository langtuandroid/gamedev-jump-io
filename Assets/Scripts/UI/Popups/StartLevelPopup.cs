using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartLevelPopup : BasePopup
{
    [SerializeField] private Button _startBtn;
    [SerializeField] private GameObject _timerAnimator;
    private void Start()
    {
        _startBtn.onClick.AddListener(()=>TapToStartBtn());
    }
    IEnumerator WaitAndAction(float duration, UnityAction action)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
    public void TapToStartBtn()
    {
        _startBtn.gameObject.SetActive(false);
        _timerAnimator.SetActive(true);
        StartCoroutine(WaitAndAction(2.5f, () => PopupsManager.Instance.HideCurrentPopup()));// timer animation duration - 2.5sec
    }
}
