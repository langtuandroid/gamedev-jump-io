using System.Collections;
using UnityEngine;

public class JIPlayerScript : MonoBehaviour
{
    public int moveSpeed;
    public int fastMoveSpeed;
    public int speedChanger;
    public int playerNo;

    public bool playerFix;
    
    private int _characterIdleAnimation; 
    private float _playerZPos;
    private Animator _animator;

    private void Start()
    {
        speedChanger = moveSpeed;
        _animator = GetComponent<Animator>();
        _characterIdleAnimation = 1;//Random.Range(1, 3);
        _animator.SetInteger("Character Animator", _characterIdleAnimation);
    }
    
    private void Update()
    {
        if(JIGameManager.Instance.play && JIGameManager.Instance.playerRunControl)
        {
            transform.Translate(0, 0, 1 * moveSpeed * Time.deltaTime);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Finish"))
        {
            int playerPosition = JIGameManager.Instance.playerPositionCounter;

            if (playerPosition == 0 && !playerFix)
            {
                JIGameManager.Instance.CalculateOtherPlayers();
            }

            JIGameManager.Instance.CalculatePlayerPositions(playerNo, playerPosition);
            JIGameManager.Instance.playerPositionCounter++;
            JIGameManager.Instance.finish = true;
            JIGameManager.Instance.playerRunControl = false;
            JIGameManager.Instance.trajectoryOn = false;
            JIGameManager.Instance.WinSFX();
            JITrajectoryScript.Instance.lineRenderer.enabled = false;
            JIGameManager.Instance.FireWorks();
            Instantiate(JIGameManager.Instance.finishEffect, new Vector3(transform.position.x, transform.position.y + 2.3f, transform.position.z), JIGameManager.Instance.finishEffect.transform.rotation);
            gameObject.SetActive(false);
        }

        if(other.gameObject.CompareTag("FastSpeed"))
        {
            moveSpeed = fastMoveSpeed;
            _animator.SetInteger("Character Animator", 7);
            StartCoroutine(ChangeToNormalSpeed());
            JIGameManager.Instance.PowerPickUpSFX();
            Destroy(other.gameObject.transform.parent.gameObject);
            JIGameManager.Instance.FastSpeedOnFunction();
            GameObject fx = Instantiate(JIGameManager.Instance.tookJumpEffect, transform.position, JIGameManager.Instance.tookJumpEffect.transform.rotation);
            fx.transform.parent = gameObject.transform;
            fx.GetComponent<Animator>().Play("TookJumpRotation");
        }

        if(other.gameObject.CompareTag("Long Jump"))
        {
            JIGameManager.Instance.LongJumpOnFunction();
            JIGameManager.Instance.PowerPickUpSFX();
            Destroy(other.gameObject.transform.parent.gameObject);
            GameObject fx = Instantiate(JIGameManager.Instance.tooklongJumpEffect, transform.position, JIGameManager.Instance.tooklongJumpEffect.transform.rotation);
            fx.transform.parent = gameObject.transform;
            fx.GetComponent<Animator>().Play("TookJumpRotation");
        }

        if(other.gameObject.CompareTag("Freeze"))
        {
            JIGameManager.Instance.FreezePlayers();
            JIGameManager.Instance.FreeeTimerOn();
            Destroy(other.gameObject.transform.parent.gameObject);
            GameObject fx = Instantiate(JIGameManager.Instance.tookFreezeEffect, transform.position, JIGameManager.Instance.tookFreezeEffect.transform.rotation);
            fx.transform.parent = gameObject.transform;
            fx.GetComponent<Animator>().Play("TookJumpRotation");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("CrystalWall"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("Destroy", true);
            JIGameManager.Instance.StoneHit();
            JIGameManager.Instance.FallSFX();
            JITrajectoryScript.Instance.PointsCounterReset();
            JITrajectoryScript.Instance.lineRenderer.enabled = false;
            Destroy(other.gameObject, 2);
        }

        if(other.gameObject.CompareTag("WaterPlatform"))
        {
            JIGameManager.Instance.CharacterFall();
            JITrajectoryScript.Instance.PointsCounterReset();
            JITrajectoryScript.Instance.lineRenderer.enabled = false;
            JIGameManager.Instance.FallSFX();
            _playerZPos = transform.position.z;
            StartCoroutine(PlayerNewPosition());
        }
    }

    private IEnumerator ChangeToNormalSpeed()
    {
        yield return new WaitForSeconds(5);
        moveSpeed = speedChanger;
        _animator.SetInteger("Character Animator", 3);
        JIGameManager.Instance.trajectoryOn = true;
        JITrajectoryScript.Instance.lineRenderer.enabled = true;
    }
    private IEnumerator PlayerNewPosition()
    {
        yield return new WaitForSeconds(3f);
        transform.position = new Vector3(0, 0, _playerZPos - 16f);
    }
}
