using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerScript : MonoBehaviour
{
    public int moveSpeed;
    public int playerNo;

    int characterIdleAnimation;
    float changedMoveSpeed;
    Animator animator;
    Rigidbody rb;
    bool run;
    Vector3 playerPos;

    private void Start()
    {
        run = true;
        changedMoveSpeed = moveSpeed;
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        characterIdleAnimation = Random.Range(1, 3);
        animator.SetInteger("Character Animator", characterIdleAnimation);
    }

    private void Update()
    {
        if (GameManager.instance.play && run && !GameManager.instance.otherPlayerFreeze)
        {
            this.transform.Translate(0, 0, changedMoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            int playerPosition = GameManager.instance.playerPositionCounter;
            if(!GameManager.instance.calculated)
            {
                GameManager.instance.CalculatePlayerPositions(playerNo, playerPosition);
                GameManager.instance.playerPositionCounter++;
            }
            this.gameObject.SetActive(false);
        }

        if(other.gameObject.CompareTag("Wall"))
        {
            run = false;
            animator.SetInteger("Character Animator", 6);
            playerPos = this.transform.position;
            StartCoroutine(StartRunning());
        }

        if(other.gameObject.CompareTag("Jumper"))
        {
            float jumpProbability = Random.Range(0.0f, 1.0f);

            if(jumpProbability > 0.4f && !other.gameObject.GetComponent<Jumper>().jumped)
            {
                changedMoveSpeed = other.gameObject.GetComponent<Jumper>().jumpForce.z;
                rb.AddForce(new Vector3(0, other.gameObject.GetComponent<Jumper>().jumpForce.y, 0), ForceMode.Impulse);
                animator.SetInteger("Character Animator", 4);
            }
            other.gameObject.GetComponent<Jumper>().jumped = true;
        }

        if (other.gameObject.CompareTag("Jumper2"))
        {
            float jumpProbability = Random.Range(0.0f, 1.0f);

            if (jumpProbability > 0.4f && !other.gameObject.GetComponent<Jumper>().jumped)
            {
                changedMoveSpeed = other.gameObject.GetComponent<Jumper>().jumpForce.z;
                rb.AddForce(new Vector3(0, other.gameObject.GetComponent<Jumper>().jumpForce.y, 0), ForceMode.Impulse);
                animator.SetInteger("Character Animator", 4);
            }
            //other.gameObject.GetComponent<Jumper>().jumped = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Platform") && GameManager.instance.play)
        {
            animator.SetInteger("Character Animator", 3);
            changedMoveSpeed = moveSpeed;
        }

        if(other.gameObject.CompareTag("CrystalWall"))
        {
            run = false;
            animator.SetInteger("Character Animator", 9);
            other.gameObject.GetComponent<Animator>().SetBool("Destroy", true);
            playerPos = this.transform.position;
            Destroy(other.gameObject, 2);
            StartCoroutine(StartRunning());
        }

        if(other.gameObject.CompareTag("WaterPlatform"))
        {
            run = false;
            animator.SetInteger("Character Animator", 6);
            playerPos = this.transform.position;
            StartCoroutine(StartRunningWaterPlatform());
        }
    }

    IEnumerator StartRunning()
    {
        yield return new WaitForSeconds(3f);
        run = true;
        this.transform.position = new Vector3(playerPos.x, 0, playerPos.z - 2.8f);
        animator.SetInteger("Character Animator", 3);
    }

    IEnumerator StartRunningWaterPlatform()
    {
        yield return new WaitForSeconds(3f);
        run = true;
        this.transform.position = new Vector3(playerPos.x, 0, playerPos.z - 12f);
        animator.SetInteger("Character Animator", 3);
    }
}
