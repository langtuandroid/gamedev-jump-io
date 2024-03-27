using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
public class TimerBoosterBehaviour : MonoBehaviour// необходимо вынести из геймМенеджера и подключить этот скрипт
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image timerImg;
    [SerializeField] private TMP_Text timerText;
    [Inject] private GameManager gameManager;
    private void Start()
    {
      //  gameManager.StartTimerEvent += (int duration) => StartTimer(duration);
    }
    public void StartTimer(int duration)
    {
        StartCoroutine(TimerCorutine(duration));
    }
    IEnumerator TimerCorutine(int duration)
    {
        canvasGroup.alpha = 1;
        float currentTime= duration;
        while(currentTime>0)
        {
            timerImg.fillAmount = currentTime / duration;
            timerText.text = Mathf.RoundToInt(currentTime).ToString();
            currentTime -= Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

}
