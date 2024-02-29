using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform player;
    public Transform finishPosition;

    int screenHeight;
    int screenWidth;

    private void Awake()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        if(screenWidth == 720 && screenHeight == 1280)
        {
            Camera.main.fieldOfView = 60;
        }

        if(screenWidth == 1080 && (screenHeight == 2340 || screenHeight == 2400))
        {
            Camera.main.fieldOfView = 70;
        }
    }

    float cameraZPosition;
    private void LateUpdate()
    {
        if(!GameManager.instance.finish)
        {
            cameraZPosition = player.position.z - 8.59f;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, cameraZPosition);
        }
        else if(GameManager.instance.finish)
        {
            StartCoroutine(FinishDelay());
        }        
    }

    IEnumerator FinishDelay()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.SlidersOff();
        GameManager.instance.StageSFX();
        this.transform.position = Vector3.Lerp(this.transform.position, finishPosition.position, 3 * Time.deltaTime);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, 0), 3 * Time.deltaTime);

        if(Vector3.Distance(this.transform.position, finishPosition.position) > 0.1)
        {
            yield return new WaitForSeconds(2);
            GameManager.instance.LevelCompletePanelOn();
        }
    }
}
