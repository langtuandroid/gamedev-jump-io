using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class JIWallCollider : MonoBehaviour
{
    public List<GameObject> blocks;

    private float _playerZPos;
    private int _counter;
    [Inject] private AudioManager audioManager;
    [Inject] private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.CompareTag("Player")) && _counter == 0)
        {
            if(!gameManager.fastSpeedOn)
            {
                gameManager.CharacterFall();
                TrajectoryManager.Instance.PointsCounterReset();
                TrajectoryManager.Instance.lineRenderer.enabled = false;
                audioManager.PlayMusic(AudioType.Fall);
                StartCoroutine(PlayerNewPosition(other));
            }            
            _counter++;            
            _playerZPos = other.gameObject.transform.position.z;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            StartCoroutine(DisableBlocks(3f));
        }

        if (other.gameObject.CompareTag("OtherPlayer") && _counter == 0)
        {
            _counter++;
            StartCoroutine(DisableBlocks(3f));
        }
    }

    private IEnumerator DisableBlocks(float timeToDisable)
    {
        yield return new WaitForSeconds(timeToDisable);
    
        gameObject.SetActive(false);
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].GetComponent<Collider>().enabled = false;
            blocks[i].GetComponent<Rigidbody>().useGravity = false;
            blocks[i].GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator PlayerNewPosition(Collider player)
    {
        yield return new WaitForSeconds(3f);
        player.transform.position = new Vector3(0, 0, _playerZPos - 2.8f);
    }
}
