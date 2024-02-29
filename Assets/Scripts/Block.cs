using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float minValue;
    public float maxValue;
    public bool blockZaxis;
    int counter;
    Rigidbody rb;

    public LayerMask layer;
    //public bool unFreeze;

    float upForce;
    float frontForce;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        upForce = Random.Range(minValue, maxValue);
        frontForce = Random.Range(minValue, maxValue);
    }

    private void OnCollisionEnter(Collision other)
    {
        if((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("OtherPlayer")) && counter == 0)
        {
            if(!blockZaxis)
            {
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
                rb.AddForce(new Vector3(0, upForce, frontForce), ForceMode.Impulse);
                counter++;
            }
            else
            {
                rb.AddForce(new Vector3(0, upForce, 0), ForceMode.Impulse);
                counter++;
            }
            
        }
    }
}
