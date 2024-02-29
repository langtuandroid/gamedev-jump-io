using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScript : MonoBehaviour
{

    public int speed;
    public bool stopMoving;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopMoving)
        {
            this.transform.Translate(0, 0, 1 * speed * Time.deltaTime);
        }
    }
}
