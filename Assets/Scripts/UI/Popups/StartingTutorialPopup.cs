using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingTutorialPopup : BasePopup
{
    [SerializeField] private Button skipBtn;

    private void Start()
    {
        skipBtn.onClick.AddListener(()=> { PopupsManager.Instance.HideCurrentPopup(); Time.timeScale = 1; });
    }
}
