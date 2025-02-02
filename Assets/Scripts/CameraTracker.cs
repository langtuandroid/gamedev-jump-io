﻿using System.Collections;
using UnityEngine;

using Zenject;
public class CameraTracker : MonoBehaviour
{
    public Transform player;
    public Transform finishPosition;

    private int _screenHeight;
    private int _screenWidth;
    private float _cameraZPosition;
    private bool _coroutineStarted = false;
    [Inject] private GameManager gameManager;
    [Inject] private AudioManager audioManager;
    private void Awake()
    {
        _screenHeight = Screen.height;
        _screenWidth = Screen.width;

        if(_screenWidth == 720 && _screenHeight == 1280)
        {
            Camera.main.fieldOfView = 60;
        }

        if(_screenWidth == 1080 && (_screenHeight == 2340 || _screenHeight == 2400))
        {
            Camera.main.fieldOfView = 70;
        }
    }

    private void LateUpdate()
    {
        if(!gameManager.finish)
        {
            _cameraZPosition = player.position.z - 8.59f;
            transform.position = new Vector3(transform.position.x, transform.position.y, _cameraZPosition);
        }
        else if(gameManager.finish)
        {
            StartCoroutine(FinishDelay());
        }        
    }

    IEnumerator FinishDelay()
    {
        yield return new WaitForSeconds(1f);
        gameManager.SlidersOff();
        audioManager.PlayMusic(AudioType.Stage);
        transform.position = Vector3.Lerp(transform.position, finishPosition.position, 3 * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 3 * Time.deltaTime);

        if(Vector3.Distance(transform.position, finishPosition.position) > 0.1 && !_coroutineStarted)
        {
            _coroutineStarted = true;
            yield return new WaitForSeconds(2);
            gameManager.LevelCompletePanelOn();
        }
    }
}
