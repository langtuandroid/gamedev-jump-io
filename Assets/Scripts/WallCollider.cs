using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    public List<GameObject> blocks;

    float playerZPos;
    int counter;
    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.CompareTag("Player")) && counter == 0)
        {
            if(!GameManager.instance.fastSpeedOn)
            {
                GameManager.instance.CharacterFall();
                TrajectoryScript.Instance.PointsCounterReset();
                TrajectoryScript.Instance.lineRenderer.enabled = false;
                GameManager.instance.FallSFX();
                StartCoroutine(PlayerNewPosition(other));
            }            
            counter++;            
            playerZPos = other.gameObject.transform.position.z;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            StartCoroutine(DisabableBlocks());
        }

        if (other.gameObject.CompareTag("OtherPlayer") && counter == 0)
        {
            counter++;
            StartCoroutine(DisabableBlocks2());
        }
    }

    IEnumerator DisabableBlocks()
    {

        yield return new WaitForSeconds(3.5f);
    
        this.gameObject.SetActive(false);
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].GetComponent<Collider>().enabled = false;
            blocks[i].GetComponent<Rigidbody>().useGravity = false;
            blocks[i].GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator DisabableBlocks2()
    {

        yield return new WaitForSeconds(3f);

        this.gameObject.SetActive(false);
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
        player.transform.position = new Vector3(0, 0, playerZPos - 2.8f);
    }
}
