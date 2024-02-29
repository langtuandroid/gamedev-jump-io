using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int moveSpeed;
    public int fastMoveSpeed;
    public int speedChanger;
    public int playerNo;

    public bool playerFix;

    //[Header("UI elements")]
    //public Image timeImage;
    //public TextMeshProUGUI timeText;
    //public int integerTimer;

    //public bool fastSpeedOn;
    int characterIdleAnimation; 
    float playerZPos;
    Animator animator;

    private void Start()
    {
        speedChanger = moveSpeed;
        animator = this.GetComponent<Animator>();
        characterIdleAnimation = Random.Range(1, 3);
        animator.SetInteger("Character Animator", characterIdleAnimation);
    }
    private void Update()
    {
        if(GameManager.instance.play && GameManager.instance.playerRunControl)
        {
            this.transform.Translate(0, 0, 1 * moveSpeed * Time.deltaTime);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Finish"))
        {
            int playerPosition = GameManager.instance.playerPositionCounter;

            if (playerPosition == 0 && !playerFix)
            {
                GameManager.instance.CalculateOtherPlayers();
            }

            GameManager.instance.CalculatePlayerPositions(playerNo, playerPosition);
            GameManager.instance.playerPositionCounter++;
            GameManager.instance.finish = true;
            GameManager.instance.playerRunControl = false;
            GameManager.instance.trajectoryOn = false;
            GameManager.instance.WinSFX();
            TrajectoryScript.Instance.lineRenderer.enabled = false;
            GameManager.instance.FireWorks();
            Instantiate(GameManager.instance.finishEffect, new Vector3(this.transform.position.x, this.transform.position.y + 2.3f, this.transform.position.z), GameManager.instance.finishEffect.transform.rotation);
            this.gameObject.SetActive(false);
        }

        if(other.gameObject.CompareTag("FastSpeed"))
        {
            moveSpeed = fastMoveSpeed;
            animator.SetInteger("Character Animator", 7);
            //GameManager.instance.trajectoryOn = false;
            //TrajectoryScript.Instance.lineRenderer.enabled = false;
            StartCoroutine(ChangeToNornalSpeed());
            GameManager.instance.PowerPickUpSFX();
            Destroy(other.gameObject.transform.parent.gameObject);
            GameManager.instance.FastSpeedOnFunction();
            GameObject fx = Instantiate(GameManager.instance.tookJumpEffect, this.transform.position, GameManager.instance.tookJumpEffect.transform.rotation);
            fx.transform.parent = this.gameObject.transform;
            fx.GetComponent<Animator>().Play("TookJumpRotation");
        }

        if(other.gameObject.CompareTag("Long Jump"))
        {
            GameManager.instance.LongJumpOnFunction();
            GameManager.instance.PowerPickUpSFX();
            Destroy(other.gameObject.transform.parent.gameObject);
            GameObject fx = Instantiate(GameManager.instance.tooklongJumpEffect, this.transform.position, GameManager.instance.tooklongJumpEffect.transform.rotation);
            fx.transform.parent = this.gameObject.transform;
            fx.GetComponent<Animator>().Play("TookJumpRotation");
        }

        if(other.gameObject.CompareTag("Freeze"))
        {
            GameManager.instance.FreezePlayers();
            GameManager.instance.FreeeTimerOn();
            Destroy(other.gameObject.transform.parent.gameObject);
            GameObject fx = Instantiate(GameManager.instance.tookFreezeEffect, this.transform.position, GameManager.instance.tookFreezeEffect.transform.rotation);
            fx.transform.parent = this.gameObject.transform;
            fx.GetComponent<Animator>().Play("TookJumpRotation");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("CrystalWall"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("Destroy", true);
            GameManager.instance.StoneHit();
            GameManager.instance.FallSFX();
            TrajectoryScript.Instance.PointsCounterReset();
            TrajectoryScript.Instance.lineRenderer.enabled = false;
            Destroy(other.gameObject, 2);
            //if(GameManager.instance.fastSpeedOn)
            //{
            //    GameManager.instance.playerRunControl = true;
            //    animator.SetInteger("Character Animator", characterIdleAnimation);
            //}
        }

        if(other.gameObject.CompareTag("WaterPlatform"))
        {
            GameManager.instance.CharacterFall();
            TrajectoryScript.Instance.PointsCounterReset();
            TrajectoryScript.Instance.lineRenderer.enabled = false;
            GameManager.instance.FallSFX();
            playerZPos = this.transform.position.z;
            StartCoroutine(PlayerNewPosition());
        }
    }

    IEnumerator ChangeToNornalSpeed()
    {
        yield return new WaitForSeconds(5);
        moveSpeed = speedChanger;
        animator.SetInteger("Character Animator", 3);
        GameManager.instance.trajectoryOn = true;
        TrajectoryScript.Instance.lineRenderer.enabled = true;
    }
    IEnumerator PlayerNewPosition()
    {
        yield return new WaitForSeconds(3f);
        this.transform.position = new Vector3(0, 0, playerZPos - 16f);
    }
}
