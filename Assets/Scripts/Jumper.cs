using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Vector3 jumpForce;

    public bool jumped;

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.CompareTag("OtherPlayer") && !jumped)
        //{
        //    jumped = true;
        //}
    }
}
