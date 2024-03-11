using System.Collections;
using UnityEngine;

public class JIOtherPlayerScript : MonoBehaviour
{
    public int moveSpeed;
    public int playerNo;

    private int _characterIdleAnimation;
    private float _changedMoveSpeed;
    private Animator _animator;
    private Rigidbody _rb;
    private bool _run;
    private Vector3 _playerPos;

    private void Start()
    {
        _run = true;
        _changedMoveSpeed = moveSpeed;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _characterIdleAnimation = 1;//Random.Range(1, 3);
        _animator.SetInteger("Character Animator", _characterIdleAnimation);
    }

    private void Update()
    {
        if (JIGameManager.Instance.play && _run && !JIGameManager.Instance.otherPlayerFreeze)
        {
            transform.Translate(0, 0, _changedMoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            if (!JIGameManager.Instance.finish) JIGameManager.Instance.finishedPlayers++;
            int playerPosition = JIGameManager.Instance.playerPositionCounter;
            if(!JIGameManager.Instance.calculated)
            {
                JIGameManager.Instance.CalculatePlayerPositions(playerNo, playerPosition);
                JIGameManager.Instance.playerPositionCounter++;
            }
            gameObject.SetActive(false);
        }

        if(other.gameObject.CompareTag("Wall"))
        {
            _run = false;
            _animator.SetInteger("Character Animator", 6);
            _playerPos = transform.position;
            StartCoroutine(StartRunning());
        }

        if(other.gameObject.CompareTag("Jumper"))
        {
            float jumpProbability = Random.Range(0.0f, 1.0f);

            if(jumpProbability > 0.4f && !other.gameObject.GetComponent<JIJumper>().jumped)
            {
                _changedMoveSpeed = other.gameObject.GetComponent<JIJumper>().jumpForce.z;
                _rb.AddForce(new Vector3(0, other.gameObject.GetComponent<JIJumper>().jumpForce.y, 0), ForceMode.Impulse);
                _animator.SetInteger("Character Animator", 4);
            }
            other.gameObject.GetComponent<JIJumper>().jumped = true;
        }

        if (other.gameObject.CompareTag("Jumper2"))
        {
            float jumpProbability = Random.Range(0.0f, 1.0f);

            if (jumpProbability > 0.4f && !other.gameObject.GetComponent<JIJumper>().jumped)
            {
                _changedMoveSpeed = other.gameObject.GetComponent<JIJumper>().jumpForce.z;
                _rb.AddForce(new Vector3(0, other.gameObject.GetComponent<JIJumper>().jumpForce.y, 0), ForceMode.Impulse);
                _animator.SetInteger("Character Animator", 4);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    { 
        if(other.gameObject.CompareTag("Platform") && JIGameManager.Instance.play)
        {
            _animator.SetInteger("Character Animator", 3);
            _changedMoveSpeed = moveSpeed;
        }

        if(other.gameObject.CompareTag("CrystalWall"))
        {
            _run = false;
            _animator.SetInteger("Character Animator", 9);
            other.gameObject.GetComponent<Animator>().SetBool("Destroy", true);
            _playerPos = transform.position;
            Destroy(other.gameObject, 2);
            StartCoroutine(StartRunning());
        }

        if(other.gameObject.CompareTag("WaterPlatform"))
        {
            _run = false;
            _animator.SetInteger("Character Animator", 6);
            _playerPos = transform.position;
            StartCoroutine(StartRunningWaterPlatform());
        }
    }

    private IEnumerator StartRunning()
    {
        yield return new WaitForSeconds(3f);
        _run = true;
        transform.position = new Vector3(_playerPos.x, 0, _playerPos.z - 2.8f);
        _animator.SetInteger("Character Animator", 3);
    }

    private IEnumerator StartRunningWaterPlatform()
    {
        yield return new WaitForSeconds(3f);
        _run = true;
        transform.position = new Vector3(_playerPos.x, 0, _playerPos.z - 12f);
        _animator.SetInteger("Character Animator", 3);
    }
}
