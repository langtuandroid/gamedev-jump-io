using System.Collections;
using UnityEngine;
using Zenject;
public class PlayerBehaviour : MonoBehaviour
{
    public int moveSpeed;
    public int fastMoveSpeed;
    public int speedChanger;
    public int playerNo;

    public bool playerFix;
    
    private int _characterIdleAnimation; 
    private float _playerZPos;
    private Animator _animator;
    [Inject] private AudioManager audioManager;
    [Inject] private GameManager gameManager;
    [Inject] private TrajectoryManager trajectoryManager;
    private void Start()
    {
        speedChanger = moveSpeed;
        _animator = GetComponent<Animator>();
        _characterIdleAnimation = 1;//Random.Range(1, 3);
        _animator.SetInteger("Character Animator", _characterIdleAnimation);
    }
    
    private void Update()
    {
        if(gameManager.play && gameManager.playerRunControl)
        {
            transform.Translate(0, 0, 1 * moveSpeed * Time.deltaTime);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Finish"))
        {
            int playerPosition = gameManager.playerPositionCounter;

            if (playerPosition == 0 && !playerFix)
            {
                gameManager.CalculateOtherPlayers();
            }

            gameManager.CalculatePlayerPositions(playerNo, playerPosition);
            gameManager.playerPositionCounter++;
            gameManager.finish = true;
            gameManager.playerRunControl = false;
            gameManager.trajectoryOn = false;
            audioManager.PlayMusic(AudioType.Win);
            trajectoryManager.lineRenderer.enabled = false;
            gameManager.FireWorks();
            Instantiate(gameManager.finishEffect, new Vector3(transform.position.x, transform.position.y + 2.3f, transform.position.z), gameManager.finishEffect.transform.rotation);
            gameObject.SetActive(false);
        }

        if(other.gameObject.CompareTag("FastSpeed"))
        {
            moveSpeed = fastMoveSpeed;
            _animator.SetInteger("Character Animator", 7);
            StartCoroutine(ChangeToNormalSpeed());
            audioManager.PlayMusic(AudioType.PowerPickUp);
            Destroy(other.gameObject.transform.parent.gameObject);
            gameManager.FastSpeedOnFunction();
            GameObject fx = Instantiate(gameManager.tookJumpEffect, transform.position, gameManager.tookJumpEffect.transform.rotation);
            fx.transform.parent = gameObject.transform;
            fx.GetComponent<Animator>().Play("TookJumpRotation");
        }

        if(other.gameObject.CompareTag("Long Jump"))
        {
            gameManager.LongJumpOnFunction();
            audioManager.PlayMusic(AudioType.PowerPickUp);
            Destroy(other.gameObject.transform.parent.gameObject);
            GameObject fx = Instantiate(gameManager.tooklongJumpEffect, transform.position, gameManager.tooklongJumpEffect.transform.rotation);
            fx.transform.parent = gameObject.transform;
            fx.GetComponent<Animator>().Play("TookJumpRotation");
        }

        if(other.gameObject.CompareTag("Freeze"))
        {
            gameManager.FreezePlayers();
            gameManager.FreeeTimerOn();
            Destroy(other.gameObject.transform.parent.gameObject);
            GameObject fx = Instantiate(gameManager.tookFreezeEffect, transform.position, gameManager.tookFreezeEffect.transform.rotation);
            fx.transform.parent = gameObject.transform;
            fx.GetComponent<Animator>().Play("TookJumpRotation");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("CrystalWall"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("Destroy", true);
            gameManager.StoneHit();
            audioManager.PlayMusic(AudioType.Fall);
           trajectoryManager.PointsCounterReset();
           trajectoryManager.lineRenderer.enabled = false;
            Destroy(other.gameObject, 2);
        }

        if(other.gameObject.CompareTag("WaterPlatform"))
        {
            gameManager.CharacterFall();
           trajectoryManager.PointsCounterReset();
           trajectoryManager.lineRenderer.enabled = false;
            audioManager.PlayMusic(AudioType.Fall);
            _playerZPos = transform.position.z;
            StartCoroutine(PlayerNewPosition());
        }
    }

    private IEnumerator ChangeToNormalSpeed()
    {
        yield return new WaitForSeconds(5);
        moveSpeed = speedChanger;
        _animator.SetInteger("Character Animator", 3);
        gameManager.trajectoryOn = true;
       trajectoryManager.lineRenderer.enabled = true;
    }
    private IEnumerator PlayerNewPosition()
    {
        yield return new WaitForSeconds(3f);
        transform.position = new Vector3(0, 0, _playerZPos - 16f);
    }
}
